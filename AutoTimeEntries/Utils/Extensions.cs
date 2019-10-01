using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace GSAutoTimeEntries.Utils
{
    public static class Extensions
    {
        public static string ToJson(this Dictionary<string, string> dictionary)
        {
            var kvs = dictionary.Select(kvp => string.Format("\"{0}\":\"{1}\"", kvp.Key, kvp.Value));
            return string.Concat("{", string.Join(",", kvs), "}");
        }

        public static Dictionary<string, string> ToStringStringDictionary(this string json)
        {
            string[] keyValueArray = json.Replace("{", string.Empty).Replace("}", string.Empty).Replace("\"", string.Empty).Split(',');
            return keyValueArray.ToDictionary(item => item.Split(':')[0], item => item.Split(':')[1]);
        }

        public static void CrossThreadInvoke<T>(this T control, Action<T> action)
            where T : Control
        {
            control.Invoke((MethodInvoker)delegate
            {
                action.Invoke(control);
            });
        }

        public static TimeSpan ArredondeParaUmQuartoDeHora(this TimeSpan tempo)
        {
            var umQuartoDeHora = new TimeSpan(0, 15, 0);

            return new TimeSpan(((tempo.Ticks + umQuartoDeHora.Ticks - 1) / umQuartoDeHora.Ticks) * umQuartoDeHora.Ticks);
        }

        public static string CriptografeBase64(this string texto)
        {
            return Convert.ToBase64String(
                ProtectedData.Protect(
                    Encoding.Unicode.GetBytes(texto), null, DataProtectionScope.CurrentUser));
        }

        public static string DescriptografeBase64(this string texto)
        {
            return Encoding.Unicode.GetString(
                ProtectedData.Unprotect(
                    Convert.FromBase64String(texto), null, DataProtectionScope.CurrentUser));
        }

        public static bool ConvertaBooleano(this char valor)
        {
            return valor == 'S' || valor == 's';
        }

        public static bool ConvertaBooleano(this string valor)
        {
            return valor == "S" || valor == "S";
        }

        public static char ConvertaBooleano(this bool valor)
        {
            return valor ? 'S' : 'N';
        }

        public static string ConvertaDataRedmine(this string data)
        {
            var splitted = data.Split('/');

            var dia = splitted[0];
            var mes = splitted[1];
            var ano = splitted[2];

            return $"{ano}-{mes}-{dia}";
        }

        public static void EsperePaginaCarregar(this ChromeDriver driver, int timeoutSec = 15)
        {
            var js = (IJavaScriptExecutor)driver;
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));

            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public static void EspereCondicao(this ChromeDriver driver, Func<IWebDriver, bool> condicao, int timeoutSec = 15)
        {
            // Func<IWebDriver, object> condicao,
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));
            wait.Until(condicao);
        }

        public static string ObtenhaStringEntre(this string text, string start, string end)
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);

            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }

        public static void EspereElementoSerExibido(this ChromeDriver chromeDriver, By by)
        {
            var contagemDeRetries = 0;

            while (true)
            {
                try
                {
                    chromeDriver.FindElement(by);
                    break;
                }
                catch (Exception)
                {
                    if (contagemDeRetries > 100)
                    {
                        break;
                    }

                    contagemDeRetries++;
                }
            }
        }

        public static void EspereElementoSerExibidoESerClicavel(this ChromeDriver chromeDriver, By by)
        {
            chromeDriver.EspereElementoSerExibido(by);

            chromeDriver.EspereCondicao(x =>
            {
                var elements = x.FindElements(by);

                return elements != null &&
                       elements.Count > 0 &&
                       elements.FirstOrDefault().Displayed &&
                       elements.FirstOrDefault().Enabled;
            });

            new WebDriverWait(chromeDriver, new TimeSpan(0, 0, 10)).Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }

        public static void EspereTempoEspecifico(this ChromeDriver driver, int segundos)
        {
            Thread.Sleep(new TimeSpan(0, 0, segundos));
        }

        public static void Focar(this Form form)
        {
            form.WindowState = FormWindowState.Normal;
            form.Activate();
            form.Focus();
        }

        public static void EspereTempoEspecifico(this ChromeOptions driver, TimeSpan tempo)
        {
            Thread.Sleep(tempo);
        }

        public static bool EhFimDeSemana(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }

        public static void HabiliteDownloadDeArquivosHeadlessNaPaginaAtual(this ChromeDriver chromeDriver)
        {
            // Habilita o chrome para permitir downloads em background
            var parametros = new Dictionary<string, object>
            {
                { "behavior", "allow" },
                { "downloadPath", AppDomain.CurrentDomain.BaseDirectory }
            };

            chromeDriver.ExecuteChromeCommand("Page.setDownloadBehavior", parametros);
        }

        #region Animação

        public enum Effect { Roll, Slide, Center, Blend }

        public static void Animate(Control ctl, Effect effect, int msec, int angle)
        {
            int flags = effmap[(int)effect];
            if (ctl.Visible)
            {
                flags |= 0x10000;
                angle += 180;
            }
            else
            {
                if (ctl.TopLevelControl == ctl)
                {
                    flags |= 0x20000;
                }
                else if (effect == Effect.Blend)
                {
                    throw new ArgumentException();
                }
            }

            flags |= dirmap[(angle % 360) / 45];
            var ok = AnimateWindow(ctl.Handle, msec, flags);
            if (!ok)
            {
                throw new Exception("Animation failed");
            }
            ctl.Visible = !ctl.Visible;
        }

        private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
        private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);

        // Personalizado

        private static bool PrecisaDoAuxiliar { get; set; }

        private static bool ToggleAuxiliar { get; set; }

        private static Point PontoAuxiliar { get; set; }

        public static void ExecuteAnimacao(
            this Control control, Point destino, EnumEfeito efeito, EnumDirecao direcao, int tempoDeExecucaoMs)
        {
            if (control.Visible)
            {
                PrecisaDoAuxiliar = true;
            }

            if (PrecisaDoAuxiliar)
            {
                if (!ToggleAuxiliar)
                {
                    control.Visible = false;
                    PontoAuxiliar = control.Location;
                }
            }

            control.Location = destino;
            Animate(control, efeito.ObtenhaEfeito(), tempoDeExecucaoMs, direcao.ObtenhaAngulo());

            ToggleAuxiliar = !ToggleAuxiliar;

            if(PrecisaDoAuxiliar)
            {
                if (!ToggleAuxiliar)
                {
                    // Localização padrão
                    control.Location = PontoAuxiliar;
                    control.Visible = true;
                }
            }
        }

        private static Effect ObtenhaEfeito(this EnumEfeito efeito)
        {
            Effect effect;
            switch (efeito)
            {
                case EnumEfeito.DESLIZAR:
                    effect = Effect.Slide;
                    break;

                case EnumEfeito.ROLAR:
                    effect = Effect.Roll;
                    break;

                case EnumEfeito.CENTRALIZAR:
                    effect = Effect.Center;
                    break;

                case EnumEfeito.MISTURAR:
                    effect = Effect.Blend;
                    break;

                default:
                    effect = Effect.Slide;
                    break;
            }

            return effect;
        }

        private static int ObtenhaAngulo(this EnumDirecao direcao)
        {
            var retorno = 0;
            switch(direcao)
            {
                case EnumDirecao.ESQUERDA_PARA_DIREITA:
                    retorno = 360;
                    break;

                case EnumDirecao.CIMA_PARA_BAIXO:
                    retorno = 270;
                    break;

                case EnumDirecao.DIREITA_PARA_ESQUERDA:
                    retorno = 180;
                    break;

                case EnumDirecao.BAIXO_PARA_CIMA:
                    retorno = 90;
                    break;
            }

            return retorno;
        }

        public static async void FadeIn(this Form o, int interval = 80)
        {
            //Object is not fully invisible. Fade it in
            while (o.Opacity < 1.0)
            {
                await Task.Delay(interval);
                o.Opacity += 0.05;
            }
            o.Opacity = 1; //make fully visible       
        }

        public static async void FadeOut(this Form o, int interval = 80)
        {
            //Object is fully visible. Fade it out
            while (o.Opacity > 0.0)
            {
                await Task.Delay(interval);
                o.Opacity -= 0.05;
            }
            o.Opacity = 0; //make fully invisible       
        }

        #endregion

        public static void AlterneEstilo(this Label controle)
        {
            if (controle.ForeColor == Color.SteelBlue)
            {
                controle.Font = new Font("Eurostile ExtendedTwo", controle.Font.Size - 2, FontStyle.Bold | FontStyle.Underline);
                controle.ForeColor = Color.CornflowerBlue;
            }
            else
            {
                controle.Font = new Font("Segoe UI", controle.Font.Size + 2, FontStyle.Regular);
                controle.ForeColor = Color.SteelBlue;
            }
        }

        public static string ConvertaParaDataPtBr(this DateTime dateTime)
        {
            var dia = dateTime.Day.ToString().PadLeft(2, '0');
            var mes = dateTime.Month.ToString().PadLeft(2, '0');
            var ano = dateTime.Year.ToString().PadLeft(2, '0');

            return $"{dia}/{mes}/{ano}";
        }

        public static DateTime ParaDateTime(this TimeSpan time)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, time.Hours, time.Minutes, time.Seconds);
        }

        public static string ObtenhaDataRedmine(this DateTime data)
        {
            var dia = data.Day;
            var mes = data.Month;
            var ano = data.Year;

            return $"{ano.ToString()}-{mes.ToString().PadLeft(2, '0')}-{dia.ToString().PadLeft(2, '0')}";
        }

        public static bool IsNumeric(this string valor)
        {
            return int.TryParse(valor, out _);
        }

        public static TimeSpan ObtenhaTimeSpan(this string tempo)
        {
            var hora = Convert.ToInt32(tempo.Split(':')[0]);
            var minuto = Convert.ToInt32(tempo.Split(':')[1]);

            return new TimeSpan(hora, minuto, 0);
        }

        public static DateTime ParaDateTime(this string time)
        {
            var splitted = time.Split('/');
            var dia = Convert.ToInt32(splitted[0]);
            var mes = Convert.ToInt32(splitted[1]);
            var ano = Convert.ToInt32(splitted[2]);

            return new DateTime(ano, mes, dia);
        }
        public static DateTime GetClosestDateTime(this DateTime timeToCompare, IList<DateTime> listOfTimes)
        {
            return listOfTimes.OrderBy(t => Math.Abs((t - timeToCompare).Ticks)).First();
        }
    }
}
