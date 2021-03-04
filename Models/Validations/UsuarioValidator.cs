using FluentValidation;
using SistemaDeControleDeTCCs.Models;

namespace ControleDeTccs.Models.Validations
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            // Validations here
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Digite o nome do usuário!")
                .Length(3, 250).WithMessage("O nome digitado deve possuir entre 3 e 250 caracteres!");

            RuleFor(x => x.Sobrenome)
                .NotEmpty().WithMessage("Digite o sobrenome do usuário!")
                .Length(6, 100).WithMessage("O sobrenome digitado deve possuir entre 6 e 100 caracteres!");

            RuleFor(x => x.Matricula)
                .NotEmpty().WithMessage("Digite a matrícula do usuário!")
                .Length(5, 25).WithMessage("A matrícula digitada deve possuir entre 5 e 25 caracteres!");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Digite o cpf do usuário!")
                .Must(validateCpf).WithMessage("O CPF informado é inválido!");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email.");
        }

        private static bool validateCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}