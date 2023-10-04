using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models.Validations
{
    /// <summary>
    /// Validação de campo de senha forte.
    /// </summary>
    public class StrongPassword : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value != null)
            {
                //Convertendo o object para string.
                var password = value.ToString();

                //aplicando regras de validação.
                return password.Length >= 8 && 
                    password.Length <= 16 &&
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsLower) &&
                    password.Any(char.IsDigit) &&
                    (
                        password.Contains("@") ||
                        password.Contains("!") ||
                        password.Contains("#") ||
                        password.Contains("$") ||
                        password.Contains("%") ||
                        password.Contains("&") ||
                        password.Contains("*") ||
                        password.Contains("+") ||
                        password.Contains("-") 
                     );

            }

            return false;
        }
    }
}
