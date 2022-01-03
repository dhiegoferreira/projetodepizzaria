﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System.Globalization;



namespace projetodepizzaria
{
    public class DAOCadastroCliente
    {

        public static string codigo;
        public static string cliEndCod;

        //definindo fora da função obter dados
        public static string StringDeConexao = "server=localhost;uid=root;pwd=;database=bancoteste";

      
        //Essa conexão não pode ser fechada, pois estamos recuperando dados da tabela em realtime.
        public static void ObterDados(DataGridView datagrid1)
        {

            string sql = "SELECT c.cli_cod AS Código, c.cli_nome AS Nome, e.end_bairro AS Bairro, e.end_rua AS Rua, e.end_numero AS Numero, c.cli_telefone AS Telefone, c.cli_sexo AS Sexo FROM clientes AS c JOIN endereco AS e ON c.cli_end_fk = e.end_cod";

            //Criando um objeto do tipo adaptador de dados e passando a string de consulta e a string de conexao
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sql, StringDeConexao);

            try
            {
                


                //populando dados na tabela
                DataTable table = new DataTable();

                dataAdapter.Fill(table);

                datagrid1.DataSource = table;



            } catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            } 

        }



        //Método para adicionar o endereço

        public static void CadastrarEndereco(String bairro, String rua, String numero)
        {
            MySqlConnection conn = new MySqlConnection(StringDeConexao);

            try
            {
                conn.Open();

                string sql = "INSERT INTO endereco(end_bairro, end_rua, end_numero) VALUES(@endbairro, @endrua, @endnumero)";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@endbairro", bairro);
                cmd.Parameters.AddWithValue("@endrua", rua);
                cmd.Parameters.AddWithValue("@endnumero", numero);


                cmd.ExecuteNonQuery();



            } catch (MySqlException ex)
               
            {
                MessageBox.Show(ex.Message);

            } finally
            {
                conn.Close();

            }

        }


        //Pegando o último código de endereço gerado
 
        public static void ResgatarCodigo()
        {
            MySqlConnection conn = new MySqlConnection(StringDeConexao);

            try
            {
                conn.Open();

                string sql = "SELECT end_cod FROM endereco WHERE end_cod ORDER BY end_cod DESC LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();


                if (rdr.Read())
                {
                    codigo = rdr[0].ToString();
                    System.Diagnostics.Debug.WriteLine("O retorno do cod é: " + codigo);
                } else
                {
                    MessageBox.Show("Não encontramos o código");
                }



            } catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }



        }



        //MÉTODO QUE VAI INSERIR O NOME,  NO BANCO DE DADOS.
        public static void CadastrarCliente(String nome, String telefone, string sexo, string sql) 
        {
            //Criando um sessão de conexão, passando a string de conexão para o objeto criado.
            MySqlConnection conn = new MySqlConnection(StringDeConexao);

            try
            {
                //Abrindo conexão com o BD
                conn.Open();

                /*
                string sql = "INSERT INTO clientes(cli_nome, cli_telefone, cli_sexo, cli_end_fk) VALUES(@clinome, @cli_telefone, @cli_sexo, @cli_end_fk)";

               */


                MySqlCommand cmd = new MySqlCommand(sql, conn);

                //Passando os valores para a string sql
                cmd.Parameters.AddWithValue("@clinome", nome);
                cmd.Parameters.AddWithValue("@cli_telefone", telefone);
                cmd.Parameters.AddWithValue("@cli_sexo", sexo);
                cmd.Parameters.AddWithValue("@cli_end_fk", codigo);


                //ExecuteNonQuery para inserção, atualização e exclusão de dados 
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cliente adicionado com sucesso!");




            //passando as excessões para uma varivável local ex
            } catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());

            } finally
            {
                //Fechando a conexão
                conn.Close();

            }

        }



        //Os TRÊS métodos abaixo irão atualizar os dados do cliente selecionado

        public static void CodigoEndereco(String nomeCliente)
        {
            String sql = "SELECT cli_end_cod FROM clientes WHERE cli_nome=@cli_nome";

            MySqlConnection conn = new MySqlConnection(StringDeConexao);

            try
            {
                //Abrindo a conexão
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cli_cod", nomeCliente);

                MySqlDataReader rdr = cmd.ExecuteReader();



                if (rdr.Read())
                {
                    //Recebendo a chave estrangeiro do cliente selecionado
                    cliEndCod = rdr[0].ToString();

                }


            } catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            } finally
            {
                conn.Close();

            }






        }



        public static void AtualizarEndereco(String bairro, String rua, String numero)
        {
            string sql = "UPDATE endereco SET end_bairro=@end_bairro, end_rua=@end_rua, end_numero=@end_numero WHERE end_cod=@end_cod";

            MySqlConnection conn = new MySqlConnection(StringDeConexao);
            try
            {

                //Abrindo a conexão
                conn.Open();
                

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@end_bairro", bairro);
                cmd.Parameters.AddWithValue("@end_rua", rua);
                cmd.Parameters.AddWithValue("@end_numero", numero);
                cmd.Parameters.AddWithValue("@end_cod", cliEndCod);


                //Para Inserção, Atualização e exclusão
                cmd.ExecuteNonQuery();




            } catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);

            }

        }


        public static void AtualizarCliente(String nome, String telefone, String sexo, String clienteCodigo)
        {
            String sql = "UPDATE clientes SET cli_nome=@cli_nome, cli_telefone=@cli_telefone, cli_sexo=@cli_sexo WHERE cli_cod=@cli_cod";

            MySqlConnection conn = new MySqlConnection(StringDeConexao);

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@cli_nome", nome);
                cmd.Parameters.AddWithValue("@cli_telefone", telefone);
                cmd.Parameters.AddWithValue("@cli_sexo", sexo);
                cmd.Parameters.AddWithValue("cli_cod", clienteCodigo);

                cmd.ExecuteNonQuery();



            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {

                conn.Close();
            }



        }
     



    }
     
}