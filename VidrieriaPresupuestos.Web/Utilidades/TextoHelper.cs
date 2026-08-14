using System.Globalization;

namespace VidrieriaPresupuestos.Web.Utilidades
{
    public static class TextoHelper
    {
        private static readonly CultureInfo CulturaEs = CultureInfo.GetCultureInfo("es-CL");

        public static string? AplicarTitleCase(string? texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return texto;
            }

            var caracteres = texto.ToLower(CulturaEs).ToCharArray();
            var inicioPalabra = true;

            for (var i = 0; i < caracteres.Length; i++)
            {
                if (char.IsWhiteSpace(caracteres[i]) || caracteres[i] == '-')
                {
                    inicioPalabra = true;
                }
                else if (inicioPalabra)
                {
                    caracteres[i] = char.ToUpper(caracteres[i], CulturaEs);
                    inicioPalabra = false;
                }
            }

            return new string(caracteres);
        }

        public static string? AplicarMayusculaInicial(string? texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return texto;
            }

            return char.ToUpper(texto[0], CulturaEs) + texto[1..];
        }
    }
}
