using System;

namespace APItest
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Primeiramente carrego a coleção de dados do webservice e passo para uma variavel.
            var listaEnderecos = Enderecos_.Carregar_ColecaoEnderecos_API();

            // Faço um bulk com a coleção de dados para gravar na tabela
            Enderecos_.SqlBulkCopy(listaEnderecos);

            Console.WriteLine("Programa executado com sucesso!");
        }
    }
}
