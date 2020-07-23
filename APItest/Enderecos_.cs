using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;


namespace APItest
{
    public class Enderecos_ : WebClient
    {
        private static string nomeDaTabelaSQL = "Enderecos";

        private static string webService = "https://viacep.com.br/ws/SP/Sao Paulo/Interlagos/json/";

        #region Colecao **

        public static List<Enderecos> Carregar_ColecaoEnderecos_API()
        {
            List<Enderecos> retorno = new List<Enderecos>();

            System.Net.WebClient webClient = new System.Net.WebClient();

            bool obteveRetorno = false;
            string urlResponse = "";

            while (obteveRetorno == false)
            {
                try
                {
                    urlResponse = Encoding.Default.GetString(webClient.DownloadData(webService));

                    obteveRetorno = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            retorno = JsonConvert.DeserializeObject<List<Enderecos>>(urlResponse);

            return retorno;
        }

        public static void SqlBulkCopy(List<Enderecos> Colecao)
        {

            if (Colecao.Count == 0) return;

            DataTable tabela = Criar_Estrutura_Tabela();

            foreach (var socEmpresa in Colecao)
            {
                DataRow linha = Preencher_Linha(tabela, socEmpresa);

                tabela.Rows.Add(linha);
            }

            using (var conn = new SqlConnection(BancoDeDados.StringDeConexao()))
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    using (SqlBulkCopy s = new SqlBulkCopy(conn))
                    {
                        s.DestinationTableName = nomeDaTabelaSQL;
                        s.BulkCopyTimeout = 600;
                        s.WriteToServer(tabela);
                    }
                }
            }
        }

        #endregion

        #region Privado ***

        private static DataTable Criar_Estrutura_Tabela()
        {
            DataTable retorno = new DataTable("Tabela");

            Criar_Coluna(retorno, typeof(string), "cep", "cep");
            Criar_Coluna(retorno, typeof(string), "logradouro", "logradouro");
            Criar_Coluna(retorno, typeof(string), "complemento", "complemento");
            Criar_Coluna(retorno, typeof(string), "bairro", "bairro");
            Criar_Coluna(retorno, typeof(string), "localidade", "localidade");
            Criar_Coluna(retorno, typeof(string), "uf", "uf");
            Criar_Coluna(retorno, typeof(string), "unidade", "unidade");
            Criar_Coluna(retorno, typeof(string), "ibge", "ibge");
            Criar_Coluna(retorno, typeof(string), "gia", "gia");

            return retorno;
        }

        private static DataRow Preencher_Linha(DataTable Tabela, Enderecos Enderecos)
        {
            DataRow retorno = Tabela.NewRow();

            retorno["cep"] = Enderecos.Cep;
            retorno["logradouro"] = Enderecos.Logradouro;
            retorno["complemento"] = Enderecos.Complemento;
            retorno["bairro"] = Enderecos.Bairro;
            retorno["localidade"] = Enderecos.Localidade;
            retorno["uf"] = Enderecos.Uf;
            retorno["unidade"] = Enderecos.Unidade;
            retorno["ibge"] = Enderecos.Ibge;
            retorno["gia"] = Enderecos.Gia;

            return retorno;
        }

        private static void Criar_Coluna(DataTable Tabela, Type Tipo, string Nome, string Caption)
        {
            DataColumn coluna = new DataColumn();

            switch (Tipo.ToString())
            {
                case "System.String":
                    coluna.DataType = Type.GetType("System.String");
                    coluna.ColumnName = Nome;
                    coluna.Caption = Caption;
                    coluna.DefaultValue = "";
                    break;

                case "System.Int32":
                    coluna.DataType = Type.GetType("System.Int32");
                    coluna.ColumnName = Nome;
                    coluna.Caption = Caption;
                    coluna.DefaultValue = 0;
                    break;

                case "System.Double":
                    coluna.DataType = Type.GetType("System.Double");
                    coluna.ColumnName = Nome;
                    coluna.Caption = Caption;
                    coluna.DefaultValue = 0;
                    break;

                case "System.DateTime":
                    coluna.DataType = Type.GetType("System.DateTime");
                    coluna.ColumnName = Nome;
                    coluna.Caption = Caption;
                    coluna.DefaultValue = DateTime.Now;
                    break;

                case "System.Boolean":
                    coluna.DataType = Type.GetType("System.Boolean");
                    coluna.ColumnName = Nome;
                    coluna.Caption = Caption;
                    coluna.DefaultValue = false;
                    break;
            }

            Tabela.Columns.Add(coluna);
        }
        #endregion


    }
}
