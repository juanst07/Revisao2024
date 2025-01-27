using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Gerador de Senha");

        int tamanho;
        while (true)
        {
            Console.Write("Digite o tamanho da senha: ");
            if (int.TryParse(Console.ReadLine(), out tamanho) && tamanho > 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Tamanho inválido. Digite um número maior que 0.");
            }
        }

        Console.Write("Incluir letras? (s/n): ");
        bool incluirLetras = Console.ReadLine().ToLower() == "s";

        Console.Write("Incluir números? (s/n): ");
        bool incluirNumeros = Console.ReadLine().ToLower() == "s";

        Console.Write("Incluir símbolos? (s/n): ");
        bool incluirSimbolos = Console.ReadLine().ToLower() == "s";

        if (!incluirLetras && !incluirNumeros && !incluirSimbolos)
        {
            Console.WriteLine("Você precisa incluir pelo menos uma categoria (letras, números ou símbolos).");
            return; 
        }

        string senha = GerarSenha(tamanho, incluirLetras, incluirNumeros, incluirSimbolos);
        Console.WriteLine($"Senha gerada: {senha}");

        Console.Write("Deseja salvar a senha? (s/n): ");
        if (Console.ReadLine().ToLower() == "s")
        {
            try
            {
                SalvarSenha(senha);
                Console.WriteLine("Senha salva no arquivo bkp.txt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar a senha: {ex.Message}");
            }
        }

        // Perguntar se deseja ver senhas salvas
        Console.Write("Deseja ver senhas salvas? (s/n): ");
        if (Console.ReadLine().ToLower() == "s")
        {
            try
            {
                RecuperarSenhas();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao recuperar as senhas: {ex.Message}");
            }
        }
    }

    static string GerarSenha(int tamanho, bool incluirLetras, bool incluirNumeros, bool incluirSimbolos)
    {
        string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numeros = "0123456789";
        string simbolos = "!@#$%^&*()-_=+[{]}\\|;:'\",<.>/?";
        string caracteres = "";

        if (incluirLetras) caracteres += letras;
        if (incluirNumeros) caracteres += numeros;
        if (incluirSimbolos) caracteres += simbolos;

        Random rand = new Random();
        char[] senha = new char[tamanho];

        // Gerar a senha com caracteres aleatórios
        for (int i = 0; i < tamanho; i++)
        {
            senha[i] = caracteres[rand.Next(caracteres.Length)];
        }

        return new string(senha);
    }

    // Função para salvar a senha no arquivo
    static void SalvarSenha(string senha)
    {
        File.AppendAllText("bkp.txt", senha + Environment.NewLine);
    }

    static void RecuperarSenhas()
    {
        if (File.Exists("bkp.txt"))
        {
            string[] senhas = File.ReadAllLines("bkp.txt");
            Console.WriteLine("Senhas salvas:");

            foreach (string senha in senhas)
            {
                Console.WriteLine(senha);
            }
        }
        else
        {
            Console.WriteLine("Nenhuma senha salva.");
        }
    }
}
