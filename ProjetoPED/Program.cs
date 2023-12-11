using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjetoPED
{
    internal class Program
    {
        public class Produto
        {
            public string nomeP; // nome produto
            public int idProduto; // id do produto
            public double precoU; // preço unitario
            public string UnidadeMed; // unidade de medida
            public double quantDis; // quantidade disponivel
            public string area; // area do produto
        }

        public class Cliente
        {
            public int idCliente; // id do cliente
            public string nomeC; // nome do cliente
            public string cidade; // cidade
            public int dataNasci; // data de nascimento do cliente
        }
        public class Venda
        {
            public int idCliente; // id do cliente
            public string nomeC; // nome do cliente
            public string areavenda; // área do produto comprado
            public string nomeP; // nome do produto comprado
            public double precoU; // preço do produto
            public double quantDis; // quantidade disponível do produto
            public double quantComp; // quantidade que foi comprada pelo cliente
            public double precoTot; // preço total gasto pelo cliente
        }
        public static System.Text.Encoding UTF8 { get; }  
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;  //permite ler caracteres especiais, tais como, o do euro, acentos.....
            Dictionary<string, List<Produto>> loja = new Dictionary<string, List<Produto>>();
            Dictionary<string, List<Cliente>> clientes = new Dictionary<string, List<Cliente>>();
            List<Venda> vendas = new List<Venda>();
            string caminho = "../../../Ficheiros/", nome, nomecid;
            int opcao, nP, idade, contaIda, contaNomes, id;
            double mediaCid;
            string cidade, areamaior, confirmation;
            bool verificar, input = false, input2 = true, xml, remover;
            lerficheiroClientes(clientes, caminho + "Clientes.txt");  //a leitura dos ficheiros é feita logo que se abre o programa para já conter dados na memoria
            lerficheiroLoja(loja, caminho + "Loja.txt");
            lerficheiroVendas(vendas, caminho + "Venda.txt");
            while (input2 == true)
            {
                try     //try catch para nao permitir introduzir letras quando pede para escolher uma opçao 
                {
                    do
                    {
                        
                        Console.WriteLine("\n║║║║║║║║║║║║║║║║║║║║║║║║║║║║║");
                        Console.WriteLine("____________ MENU ___________");
                        Console.WriteLine("Escolha a opção ");
                        Console.WriteLine("1 - Secção dos Clientes");
                        Console.WriteLine("2 - Secção dos Produtos");
                        Console.WriteLine("3 - Secção das Compras");
                        Console.WriteLine("4 - Gravar para XML");
                        Console.WriteLine("0 - Sair");
                        Console.WriteLine("║║║║║║║║║║║║║║║║║║║║║║║║║║║║║\n");
                        opcao = Convert.ToInt32(Console.ReadLine());
                        switch (opcao)
                        {
                            case 1:
                                do
                                {
                                    Console.WriteLine("\n║║║║║║║║║║║║║║║║║║║║║║║║║║║║║");
                                    Console.WriteLine("__________ CLIENTE __________");
                                    Console.WriteLine("Escolha a opção ");
                                    Console.WriteLine("1 - Introduzir um novo Cliente");
                                    Console.WriteLine("2 - Mostrar Clientes");
                                    Console.WriteLine("3 - Cidade com Mais Clientes");
                                    Console.WriteLine("4 - Média Idades dos Clientes");
                                    Console.WriteLine("5 - Média Idades dos Clientes por Cidade");
                                    Console.WriteLine("6 - Pessoas com Idade Superior a...");
                                    Console.WriteLine("7 - Contar Pessoas de uma Cidade pelo Nome");
                                    Console.WriteLine("8 - Verificar se Existe um Cliente");
                                    Console.WriteLine("9 - Cliente que gastou mais");
                                    Console.WriteLine("10 - Remover Cliente");
                                    Console.WriteLine("0 - Menu Anterior");
                                    Console.WriteLine("║║║║║║║║║║║║║║║║║║║║║║║║║║║║║\n");
                                    opcao = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("\n");
                                    switch (opcao)
                                    {
                                        case 1:
                                            Console.Write("Números de Clientes a Adicionar: ");
                                            nP = Convert.ToInt32(Console.ReadLine());
                                            AdicionarClienteNovo(clientes, nP, caminho);
                                            break;
                                        case 2:
                                            mostrarClientes(clientes);
                                            break;
                                        case 3:
                                            Console.WriteLine("A cidade com mais clientes é " + cidademaisClientes(clientes) + ".");
                                            break;
                                        case 4:
                                            mediaCid = mediaidadesclientes(clientes);
                                            Console.WriteLine("A média de idades de todos os clientes é de " + Math.Round(mediaCid) + " anos.");
                                            break;
                                        case 5:
                                            Console.WriteLine("Qual cidade quer saber a média de idades?");
                                            cidade = Console.ReadLine().ToLower();
                                            verificar = verificarCidade(clientes, cidade);
                                            if (verificar == true)
                                            {
                                                mediaCid = mediaidadescidade(clientes, cidade);
                                                Console.WriteLine("\nA média de idades de " + cidade + " é de " + Math.Round(mediaCid) + " anos.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nEssa cidade não existe!");
                                            }
                                            break;
                                        case 6:
                                            Console.WriteLine("De qual idade deseja verificar quantos clientes existem acima dela?");
                                            idade = Convert.ToInt32(Console.ReadLine());
                                            contaIda = numeroPessoasAcimaIdade(clientes, idade);
                                            Console.WriteLine("\nExistem " + contaIda + " pessoas com idade acima da indicada ("+idade+" anos).");
                                            break;
                                        case 7:
                                            Console.WriteLine("Qual nome deseja verificar se existe?");
                                            nome = Console.ReadLine();
                                            Console.WriteLine("\nE qual a cidade?");
                                            nomecid = Console.ReadLine();
                                            contaNomes = contarNomes(clientes, nome, nomecid);
                                            Console.WriteLine("\nCom o nome (" + nome + ") existe " + contaNomes + " pessoa(s) na cidade " + nomecid + ".");
                                            break;
                                        case 8:
                                            Console.WriteLine("Qual nome deseja verificar que existe?");
                                            nome = Console.ReadLine();
                                            verificar = verificarCliente(clientes, nome);
                                            if (verificar == true)
                                            {
                                                Console.WriteLine("\nEsse nome existe.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nEsse nome não existe.");
                                            }
                                            break;
                                        case 9:
                                            clienteMaisgasto(vendas);
                                            break;
                                        case 10:
                                            Console.WriteLine("Qual nome deseja remover da base de dados?");
                                            nome = Console.ReadLine();
                                            remover = removerCliente(clientes, nome);
                                            if (remover == true)
                                            {
                                                Console.WriteLine("\nCliente Removido!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nCliente Não Existe!");
                                            }
                                            break;
                                        case 0:
                                            break;
                                        default:
                                            Console.WriteLine("\nOpção Inválida!");
                                            break;
                                    }
                                } while (opcao != 0);
                                break;
                            case 2:
                                do
                                {
                                    Console.WriteLine("\n║║║║║║║║║║║║║║║║║║║║║║║║║║║║║║║");
                                    Console.WriteLine("__________ PRODUTOS ___________");
                                    Console.WriteLine("Escolha a opção ");
                                    Console.WriteLine("1 - Introduzir um novo produto");
                                    Console.WriteLine("2 - Mostrar os Produtos");
                                    Console.WriteLine("3 - Área com Mais Produtos");
                                    Console.WriteLine("4 - Verificar se um Produto Existe");
                                    Console.WriteLine("5 - Produtos com Mais Receita");
                                    Console.WriteLine("6 - Qual o produto mais caro?");
                                    Console.WriteLine("7 - Área mais Vendida");
                                    Console.WriteLine("8 - Preço Médio dos Produtos por Área");
                                    Console.WriteLine("9 - Adicionar Stock a um Produto");
                                    Console.WriteLine("10 - Remover Produto");
                                    Console.WriteLine("0 - Menu Anterior");
                                    Console.WriteLine("║║║║║║║║║║║║║║║║║║║║║║║║║║║║║║║\n");
                                    opcao = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("\n");
                                    switch (opcao)
                                    {
                                        case 1:
                                            Console.Write("Número de Produtos a Adicionar: ");
                                            nP = Convert.ToInt32(Console.ReadLine());
                                            AdicionarProdutoNovo(loja, nP, caminho);
                                            break;
                                        case 2:
                                            mostrarProdutos(loja);
                                            break;
                                        case 3:
                                            areamaior = areamaisProdutos(loja);
                                            Console.WriteLine("A área com mais produtos é " + areamaior + ".");
                                            break;
                                        case 4:
                                            Console.WriteLine("Qual o id do produto que deseja verificar se existe?");
                                            id = Convert.ToInt32(Console.ReadLine());
                                            verificarProdPorCodigo(loja, id);
                                            break;
                                        case 5:
                                            geraMaisReceita(vendas);
                                            break;
                                        case 6:
                                            prodMaisCaro(loja);
                                            break;
                                        case 7:
                                            areaMaisvendida(vendas);
                                            break;
                                        case 8:
                                            mediaPrecoArea(loja);
                                            break;
                                        case 9:
                                            Console.WriteLine("Qual produto que deseja adicionar stock?");
                                            nome = Console.ReadLine();
                                            verificar = adicionarStock(loja, nome);
                                            if (verificar == true)
                                            {
                                                Console.WriteLine("\nStock adicionado com sucesso!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nEsse produto não existe, vá adicioná-lo!");
                                            }
                                            break;
                                        case 10:
                                            Console.WriteLine("Qual produto deseja remover da base de dados?");
                                            nome = Console.ReadLine();
                                            remover = removerProduto(loja, nome);
                                            if (remover == true)
                                            {
                                                Console.WriteLine("\nProduto Removido!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nProduto Não Existe!");
                                            }
                                            break;
                                        case 0:
                                            break;
                                        default:
                                            Console.WriteLine("Opção Inválida!");
                                            break;
                                    }
                                } while (opcao != 0);
                                break;
                            case 3:
                                do
                                {
                                    Console.WriteLine("\n║║║║║║║║║║║║║║║║║║║║║║║║║║║║║");
                                    Console.WriteLine("___________ VENDA ___________");
                                    Console.WriteLine("Escolha a opção ");
                                    Console.WriteLine("1 - Comprar um Produto");
                                    Console.WriteLine("2 - Verificar o Stock");
                                    Console.WriteLine("3 - Histórico de Compras do Cliente");
                                    Console.WriteLine("0 - Menu Anterior");
                                    Console.WriteLine("║║║║║║║║║║║║║║║║║║║║║║║║║║║║║\n");
                                    opcao = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("\n");
                                    switch (opcao)
                                    {
                                        case 1:
                                            Comprar(vendas, loja, clientes);
                                            break;
                                        case 2:
                                            stockBaixoMostrar(loja);
                                            break;
                                        case 3:
                                            Console.WriteLine("Qual o nome do cliente que deseja verificar o histórico de compras?");
                                            nome = Console.ReadLine();
                                            input = mostrarComprasCliente(vendas, nome);
                                            if (input == false)
                                            {
                                                Console.WriteLine("\nEsse cliente ainda não fez nenhuma compra!\n");
                                            }
                                            break;
                                        case 0:
                                            break;
                                        default:
                                            Console.WriteLine("Opção Inválida!");
                                            break;
                                    }
                                } while (opcao != 0);
                                break;
                            case 4:
                                xml = gravarFicheiroXML(loja, clientes, vendas, caminho + "LojaAlimentar.XML");
                                if (xml == true)
                                {
                                    Console.WriteLine("\nA base de dados foi gravada com sucesso em formato XML.");
                                }
                                break;
                            case 0:
                                Console.WriteLine("\nDeseja gravar na base de dados? Insira S/N ");
                                confirmation= Console.ReadLine();
                                if (confirmation == "S" || confirmation == "s")
                                {
                                    GravarFicheiroVenda(vendas, caminho + "Venda.txt");
                                    GravarFicheiroProduto(loja, caminho + "Loja.txt");
                                    GravarFicheiroCliente(clientes, caminho + "Clientes.txt");
                                }
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Opção Inválida!");
                                Console.WriteLine("\n");
                                break;
                        }
                    } while (opcao != 0);
                }
                catch (Exception)
                {
                    Console.WriteLine("Só pode utilizar números!");
                    Console.WriteLine("\n");
                }
            }
        }


        public static void lerficheiroClientes(Dictionary<string, List<Cliente>> clientes, string caminho)
        {
            StreamReader F = new StreamReader(caminho); 
            try
            {
                string linha, cid;
                string[] parts;
                List<Cliente> lista;
                cid = F.ReadLine(); //1ª cidade a ler
                while (cid != null) //enquanto a cidade for diferente de null
                {
                    lista = new List<Cliente>();
                    clientes.Add(cid, lista); // novo par do dicionario
                    linha = F.ReadLine();//le linha seguinte, neste caso as informaçoes dos clientes da cidade
                    while (linha.Equals("----------------------------------------------") == false) //enquanto não chega ao fim da cidade
                    {
                        parts = linha.Split(';');
                        Cliente p = new Cliente();
                        p.idCliente = Convert.ToInt32(parts[0]);
                        p.nomeC = parts[1].Trim();
                        p.cidade = parts[2];
                        p.dataNasci = Convert.ToInt32(parts[3]);
                        p.cidade = cid;
                        clientes[cid].Add(p);
                        linha = F.ReadLine();
                    }
                    //aqui linha = ----------------------------------------------
                    // e a proxima linha lida é a cidade
                    cid = F.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ficheiro nao encontrado!"); // o caminho pode estar incorreto
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro de I/O: " + e.Message);

            }
            catch (Exception err)
            {
                Console.WriteLine("Erro geral na execução: " + err.Message); //pode haver espaços no fim ou vírgulas/pontos incorretos
            }
            finally
            {
                if (F != null) 
                    F.Close();
            }
        }

        public static void lerficheiroLoja(Dictionary<string, List<Produto>> dici, string caminho)
        {
            StreamReader F = new StreamReader(caminho);
            try
            {
                string linha, area;
                string[] parts;
                List<Produto> lista;
                area = F.ReadLine();
                while (area != null)
                {
                    lista = new List<Produto>();
                    dici.Add(area, lista);
                    linha = F.ReadLine();
                    while (linha.Equals("----------------------------------------------") == false)
                    {
                        parts = linha.Split(';');
                        Produto p = new Produto();
                        p.idProduto = Convert.ToInt32(parts[0]);
                        p.nomeP = parts[1].Trim();
                        p.precoU = Convert.ToDouble(parts[2]);
                        p.UnidadeMed = parts[3];
                        p.quantDis = Convert.ToDouble(parts[4]);
                        dici[area].Add(p);
                        linha = F.ReadLine();
                    }
                    area = F.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ficheiro não encontrado!");
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro de I/O: " + e.Message);
            }
            catch (Exception err)
            {
                Console.WriteLine("Erro geral na execução: " + err.Message);
            }
            finally
            {
                if (F != null) 
                    F.Close();
            }
        }

        public static void lerficheiroVendas(List<Venda> lista, string caminho)
        {
            StreamReader F = new StreamReader(caminho);
            try
            {
                String linha;
                String[] parts;
                while ((linha = F.ReadLine()) != null)
                {
                    parts = linha.Split(';');
                    Venda p = new Venda();
                    p.idCliente = Convert.ToInt32(parts[0]);
                    p.nomeC = parts[1];
                    p.areavenda = parts[2];
                    p.nomeP = parts[3];
                    p.precoU = Convert.ToDouble(parts[4]);
                    p.quantDis = Convert.ToDouble(parts[5]);
                    p.quantComp = Convert.ToDouble(parts[6]);
                    p.precoTot = Convert.ToDouble(parts[7]);
                    lista.Add(p);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ficheiro não encontrado!");
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro de I/O: " + e.Message);

            }
            catch (Exception err)
            {
                Console.WriteLine("Erro geral na execução: " + err.Message);
            }
            finally
            {
                if (F != null) 
                    F.Close();
            }
        }

        public static void AdicionarProdutoNovo(Dictionary<string, List<Produto>> dici, int nP, string caminho)
        {
            List<Produto> lista;   // lista de produtos
            string area;
            bool input = true;
            bool valid;
            for (int i = 0; i < nP; i++)
            {
                Produto p = new Produto();  //cria um objeto da classe Produto
                do 
                {
                    Console.Write("\nÁrea: ");
                    p.area = Console.ReadLine();
                    area = p.area.Trim().ToLower();
                } while (p.area.Trim() == "");

                while (input == true)
                {
                    try //apenas aceita numeros
                    {
                        do
                        {
                            

                            Console.Write("Id: ");
                            p.idProduto = Convert.ToInt32(Console.ReadLine());
                            valid = verificarIdProdutoExistente(dici, p.idProduto);
                            input = false;
                            if(valid == true)
                            {
                                Console.WriteLine("Esse id já existe. Escolha outro.");
                            }
                        } while (valid == true);

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Introduza um número!");
                    }

                }
                input = true;

                do
                {
                    Console.Write("Nome do Produto: ");
                    p.nomeP = Console.ReadLine();
                } while (p.nomeP.Trim() == "");   //Trim - elimina espaços em branco // Não aceita nomes vazios 

                while (input == true)
                {
                    try
                    {
                        do
                        {
                            Console.Write("Preço: ");
                            p.precoU = Convert.ToDouble(Console.ReadLine());
                            input = false;
                        } while (p.precoU <= 0);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Introduza um número!");
                    }

                }
                input = true;

                do
                {
                    Console.Write("Unidade de Medida(kg ou und): ");
                    p.UnidadeMed = Console.ReadLine();

                } while (p.UnidadeMed.Trim().ToLower() != "kg" && p.UnidadeMed.Trim().ToLower() != "und"); //só aceita ser introduzido kg (quilogramas) ou und (unidades)

                while (input == true)
                {
                    try
                    {
                        do
                        {
                            do
                            {
                                Console.Write("Stock: ");
                                p.quantDis = Convert.ToDouble(Console.ReadLine());
                                input = false;
                                if (p.quantDis / Math.Truncate(p.quantDis) != 1 && p.UnidadeMed == "und")
                                {
                                    Console.WriteLine("Tem de introduzir um valor unitário.");
                                }
                            } while (p.quantDis / Math.Truncate(p.quantDis) != 1 && p.UnidadeMed == "und"); //Math.truncate -> apenas le parte inteira dos numeros|| com isto, quando a unidade de medida escolhida for a unidade, apenas aceita numeros inteiros
                        } while (p.quantDis <= 0);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Introduza um número!");
                    }

                }
                input = true;

                if (dici.ContainsKey(area.ToLower()) == false) // se não existir a chave(área) no dicionario 
                {
                    lista = new List<Produto>();  // cria a lista dos produtos
                    lista.Add(p);                // adiciona o produto à lista
                    dici.Add(area, lista);     // adiciona o par ao dicionário
                }
                else                             // se já existir a chave(área) no dicionário 
                    dici[area].Add(p);         // adiciona a o produto à lista do dicionário 
            }
            string confirmation;  // este pedaço de codigo serve para pedir ao utilizador se quer guardar o que fez para o ficheiro 
            Console.WriteLine("\nDeseja gravar na base de dados? Insira S/N ");
            confirmation= Console.ReadLine().ToLower();
                if (confirmation.ToLower() == "s")
                {
                GravarFicheiroProduto (dici, caminho + "Loja.txt");
                Console.WriteLine("Guardado com sucesso!");
                }
                else
                {
                Console.WriteLine("Nada foi guardado!");
            }
        }

        public static void GravarFicheiroProduto(Dictionary<string, List<Produto>> produto, string caminho)
        {
            StreamWriter writer = new StreamWriter(caminho);
            try
            {
                foreach (KeyValuePair<string, List<Produto>> prod in produto) // percorre o dicionário
                {
                    writer.WriteLine(prod.Key); // escreve a chave do dicionário, neste caso a área do produto
                    foreach (Produto prodw in prod.Value)
                    {
                        writer.WriteLine("\t" + prodw.idProduto + ";" + prodw.nomeP + ";" + prodw.precoU + ";" + prodw.UnidadeMed + ";" + prodw.quantDis); // escreve as propriedades da classe Produto
                    }
                    writer.WriteLine("----------------------------------------------"); // quer dizer que acaba a área acabou
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message, "Erro!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        
        public static void AdicionarClienteNovo(Dictionary<string, List<Cliente>> clientes, int nP, string caminho)
        {
            List<Cliente> lista;
            string cidade;
            bool input = true;
            for (int i = 0; i < nP; i++)
            {
                Cliente p = new Cliente();
                p.idCliente = incrementaID(clientes);
                Console.WriteLine("\nFoi lhe atribuido o id: "+p.idCliente);
                input = true;

                do
                {
                    Console.Write("Nome do Cliente: ");
                    p.nomeC = Console.ReadLine();
                } while (p.nomeC.Trim() == "");

                do
                {
                    Console.Write("Cidade: ");
                    p.cidade = Console.ReadLine();
                    cidade = p.cidade.Trim().ToLower();
                } while (p.cidade.Trim() == "");

                while (input == true)
                {
                    try 
                    {
                        do
                        {
                            Console.Write("Data de Nascimento (no formato yyyymmdd): "); // escolhemos para data de nascimento o formato yyyymmdd, para facilitar os cálculos da média de idades e o método funcionar a qualquer altura do ano (exemplo: 19980617 = 1998/06/17)
                            p.dataNasci = Convert.ToInt32(Console.ReadLine());
                            input = false;
                        } while (p.dataNasci/10000 > 2004 && p.dataNasci/10000 < 1900);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Introduza uma data válida!");
                    }

                }
                input = true;

                if (clientes.ContainsKey(cidade) == false)
                {
                    lista = new List<Cliente>();
                    lista.Add(p);
                    clientes.Add(cidade, lista);
                }
                else
                    clientes[cidade].Add(p);
                string confirmation = "";
                Console.WriteLine("\nDeseja gravar na base de dados? Insira S/N ");
                confirmation = Console.ReadLine();
                if (confirmation.ToLower() == "s")
                {
                    GravarFicheiroCliente(clientes, caminho + "Clientes.txt");
                    Console.WriteLine("Guardado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Nada foi guardado!");
                }
            }
        }

        public static void GravarFicheiroCliente(Dictionary<string, List<Cliente>> clientes, string caminho)
        {
            StreamWriter writer = new StreamWriter(caminho);

            try
            {

                foreach (KeyValuePair<string, List<Cliente>> cliente in clientes)
                {
                    writer.WriteLine(cliente.Key);
                    foreach (Cliente pes in cliente.Value)
                    {
                        writer.WriteLine("\t" + pes.idCliente + ";" + pes.nomeC + ";" + pes.cidade + ";" + pes.dataNasci);
                    }
                    writer.WriteLine("----------------------------------------------");
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message, "Erro!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            finally                                         // Quando nao existe mais nada a adicionar o ficheiro fecha
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static void GravarFicheiroVenda(List<Venda> vendas, string caminho)
        {
            StreamWriter writer = new StreamWriter(caminho);

            try
            { 
                foreach (Venda v in vendas)
                {
                    writer.WriteLine(v.idCliente + ";" + v.nomeC + ";" + v.areavenda + ";" + v.nomeP + ";" + v.precoU + ";" + v.quantDis + ";" + v.quantComp + ";" + v.precoTot);
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message, "Erro!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        
        public static void mostrarProdutos(Dictionary<string, List<Produto>> loja)
        {
            foreach (KeyValuePair<string, List<Produto>> mostrar in loja)
            {
                Console.WriteLine(mostrar.Key + ":");
                foreach (Produto prod in mostrar.Value)
                {

                    Console.WriteLine("\t" + "ID "+ prod.idProduto + " - " + prod.nomeP + " - " + prod.precoU + "€" + " - " + prod.quantDis + " " + prod.UnidadeMed);
                }
            }
        }

        public static string cidademaisClientes(Dictionary<string, List<Cliente>> clientes)
        {
            int maior = -100;
            string cidademaior = "";
            foreach (KeyValuePair<string, List<Cliente>> dici in clientes)
            {
                if (dici.Value.Count() > maior)      //.count conta o numero de clientes por cidade neste caso, indica o tamanho da lista
                {
                    maior = dici.Value.Count();
                    cidademaior = dici.Key; // guardamos nesta variável o nome da cidade correspondente a mais clientes para depois usarmos
                }
            }
            return cidademaior;
        }

        public static double mediaidadesclientes(Dictionary<string, List<Cliente>> clientes)
        {
            int soma = 0, dataNasc, hoje, idade, contclientes = 0;
            double media;
            foreach (KeyValuePair<string, List<Cliente>> mostrar in clientes)
            {
                foreach (Cliente pes in mostrar.Value)
                {
                    dataNasc = pes.dataNasci;
                    hoje = int.Parse(DateTime.Now.ToString("yyyyMMdd")); // este código atribui a data de hoje à variavel "hoje"
                    idade = (hoje - dataNasc) / 10000;    //Esta é uma maneira estranha de fazer, mas se se formatar a data para yyyyymmdd e subtrair a data de nascimento da data atual, e se ignorar os últimos 4 dígitos do resultado (obtendo a parte inteira da divisao por 1000) chegamos à idade da pessoa :)
                    soma = soma + idade;
                    contclientes++;                     // contamos os clientes
                }
            }
            media = (double)soma / contclientes;
            return media;
        }

        public static double mediaidadescidade(Dictionary<string, List<Cliente>> clientes, string cidade)
        {
            int soma = 0, dataNasc, hoje, idade;
            double media;
            foreach (KeyValuePair<string, List<Cliente>> mostrar in clientes)
            {
                foreach (Cliente pes in mostrar.Value)
                {
                    if (pes.cidade == cidade)
                    {
                        dataNasc = pes.dataNasci;
                        hoje = int.Parse(DateTime.Now.ToString("yyyyMMdd")); // este código atribui a data de hoje à variavel "hoje"
                        idade = (hoje - dataNasc) / 10000;    //Esta é uma maneira estranha de fazer, mas se se formatar a data para yyyyymmdd e subtrair a data de nascimento da data atual, e se ignorar os últimos 4 dígitos do resultado (obtendo a parte inteira da divisao por 1000) chegamos à idade da pessoa :)
                        soma = soma + idade;
                    }
                }
            }
            media = (double)soma / clientes[cidade].Count;
            return media;
        }

        public static string areamaisProdutos (Dictionary<string, List<Produto>> loja)
        {
            int maior = -100;
            string maiorarea = "";                                       // "" espaço vazio
            foreach (KeyValuePair<string, List<Produto>> prod in loja)
            {
                if (prod.Value.Count > maior)
                {
                    maior = prod.Value.Count;
                    maiorarea = prod.Key;
                }
            }
            return maiorarea;
        }

        public static string Comprar(List<Venda> lista, Dictionary<string, List<Produto>> loja, Dictionary<string, List<Cliente>> clientes)
        {
            Venda v = new Venda();          //cria uma nova venda 
            string nome, nomeProd;
            Console.WriteLine("Qual o seu nome?");
            nome = Console.ReadLine();
            string ValBuy = "";                  // variavel nao encontrou
            bool encontrou = false;
            foreach (KeyValuePair<string, List<Cliente>> mostrar in clientes)
            {
                foreach (Cliente cliente in mostrar.Value)
                {
                    if (cliente.nomeC.ToLower() == nome.ToLower())       // Procura se o nome introduzido já existe
                    {                                                          //Se existe vai buscar os dados do cliente,id,nome.
                        v.idCliente = cliente.idCliente;
                        encontrou = true;
                        v.nomeC = cliente.nomeC;
                    }
                }
            }
            if (encontrou == false)                                             //Se nao encontrou, return -  ValBuy
            {
                ValBuy = "\nEsse cliente não existe na base de dados da loja.";
                Console.WriteLine(ValBuy);
                Console.WriteLine("Para criar uma ficha de cliente vá ao Menu Principal, Secção dos Clientes e escolha a opção 1 (Criar Cliente Novo).");
                return ValBuy;
            }
            encontrou = false;                                                  // Variavel passa a falso , para a validaçao do nome do produto
            Console.WriteLine("\nProdutos da Loja\n");
            mostrarProdutos(loja);
            Console.WriteLine("\nQual produto deseja comprar?");
            nomeProd = Console.ReadLine();
            foreach (KeyValuePair<string, List<Produto>> mostra in loja)
            {
                foreach (Produto prod in mostra.Value)
                {
                    if (prod.nomeP.ToLower().Trim() == nomeProd.ToLower().Trim())
                    {
                        encontrou = true;                                               //Se o produto for igual a um produto existente , vais busccar os dados do produto e altera os dados consoante as quantidades compradas
                        v.nomeP = prod.nomeP;
                        v.precoU = prod.precoU;
                        v.quantDis = prod.quantDis;
                        v.areavenda = mostra.Key;
                        Console.WriteLine("\nQuanto deseja comprar?");
                        v.quantComp = Convert.ToDouble(Console.ReadLine());
                        while (v.quantComp > v.quantDis || v.quantComp <= 0)                                 //Só deixa comprar se houver stock, quantidade a comprar, menor ou igual ao stock
                        {
                            if (v.quantComp <= 0)
                            {
                                Console.WriteLine("Introduza uma quantidade válida!");
                                Console.WriteLine("Quanto deseja comprar?");
                                v.quantComp = Convert.ToDouble(Console.ReadLine());
                            }
                            else if (v.quantDis == 0)
                            {
                                ValBuy = "\nEsse produto não tem stock, não pode comprar!";
                                Console.WriteLine(ValBuy);
                                return ValBuy;
                            }
                            else
                            {
                                Console.WriteLine("\nSó existe " + v.quantDis + " disponível na loja. Insira uma quantidade válida!");
                                Console.WriteLine("Quanto deseja comprar?");
                                v.quantComp = Convert.ToDouble(Console.ReadLine());
                            }

                        }

                        prod.quantDis = prod.quantDis - v.quantComp;            // altera os dados do produto e da venda consoante a compra
                        v.precoTot = prod.precoU * v.quantComp;
                        v.quantDis = prod.quantDis;
                    }

                }
            }
            if (encontrou == false)                                 //se o produto nao existir, diz ao utilizador
            {
                ValBuy = "\nEsse produto não existe!";
                Console.WriteLine(ValBuy);
                return ValBuy;

            }
            lista.Add(v);
            ValBuy = "Compra efetuada!";
            Console.WriteLine(ValBuy);
            return ValBuy;


        }

        public static void stockBaixoMostrar(Dictionary<string,List<Produto>> loja)
        {
            Console.WriteLine("Produtos que necessitam de repor stock!!!!");
            foreach (KeyValuePair<string, List<Produto>> mostrar in loja)
            {
                foreach (Produto prod in mostrar.Value)
                {
                    if (prod.quantDis < 3)
                    {
                        Console.WriteLine("\nNome do Produto: " + prod.nomeP);
                        Console.WriteLine("ID: " + prod.idProduto);
                        Console.WriteLine("Preço: " + prod.precoU + "€ " + prod.UnidadeMed);
                        Console.WriteLine("Quantidade Disponível: " + prod.quantDis);
                    }
                }
            }
        }

        public static bool mostrarComprasCliente(List<Venda> vendas, string nome)
        {
            bool input = false;
            foreach(Venda v in vendas)
            {
                    if (nome.ToLower() == v.nomeC.ToLower())
                    {
                        Console.WriteLine("\nID: " + v.idCliente);
                        Console.WriteLine("Nome do Cliente: " + v.nomeC);
                        Console.WriteLine("Nome do Produto: " + v.nomeP);
                        Console.WriteLine("Quantidade Comprada: " + v.quantComp);
                        Console.WriteLine("Preço: " + v.precoU + "€");
                        Console.WriteLine("Total Gasto: " + v.precoTot + "€");
                    input = true;
                    }
            }
            return input;
        }

        public static int numeroPessoasAcimaIdade(Dictionary<string,List<Cliente>> clientes, int idadeIntr)
        {
            int contaP=0;
            int dataNasc, hoje, idade;
            foreach (KeyValuePair<string, List<Cliente>> mostrar in clientes)
            {
                foreach (Cliente pes in mostrar.Value)
                {
                    dataNasc = pes.dataNasci;
                    hoje = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                    idade = (hoje - dataNasc) / 10000;                                              // calcula a idade da pessoa 
                    if (idadeIntr < idade)                                                          // Se idade introduzida for menor que a idade do cliente, é adicionado mais um ao contador

                    {
                        contaP++;
                    }
                }

            }
            return contaP;
        }

        public static int contarNomes(Dictionary<string,List<Cliente>> clientes, string nome, string nomecid)
        {
            int count = 0;
            string[] partes;
            foreach(KeyValuePair<string, List<Cliente>> mostrar in clientes)
            {
                foreach(Cliente pes in mostrar.Value)                                         
                {
                    partes = pes.nomeC.Split(' ');                                              // Dividir o nome pelo espaço
                    if (nomecid.ToLower() == pes.cidade.ToLower())                              // Confirma se a cidade 
                    {
                        for (int n = 0; n < partes.Length; n++)                                 // Partes igual a espaço, para atribuir tamanho do vetor
                        {
                            if (partes[n].ToLower() == nome.ToLower())                          // Para cada nome(Rui Costa) verifica se há algum nome(Rui ,Ou, e Costa)  
                            {                                                                                  //que se seja igual ao nome que foi introduzido pelo utilizador
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }

        public static bool verificarCliente(Dictionary<string, List<Cliente>> clientes, string nome)
        {
            bool verificar = false;
            foreach (KeyValuePair<string, List<Cliente>> mostrar in clientes)
            {
                foreach (Cliente cliente in mostrar.Value)
                {
                    if (cliente.nomeC.ToLower() == nome.ToLower())                                  //Verificara se existe um cliente com o nome introduzido
                    {
                        verificar = true;

                    }
                }
            }
            return verificar;
        }
        
        public static void verificarProdPorCodigo(Dictionary<string, List<Produto>> loja, int id)
        {
            bool verifica = false;
            foreach(KeyValuePair<string,List<Produto>> mostrar in loja)
            {
                foreach(Produto prod in mostrar.Value)
                {
                    if (prod.idProduto == id)                                                   // Verifica se existe o produto pelo id
                    {
                        verifica = true;
                        Console.WriteLine("\nEsse produto existe!");
                        Console.WriteLine("\nÁrea: " + mostrar.Key);
                        Console.WriteLine("Nome do Produto: " + prod.nomeP);
                        Console.WriteLine("Quantidade Disponível: " + prod.quantDis);
                    }           
                }
            }
            if(verifica== false)
            {
                Console.Write("\nEsse id não está associado a nenhum produto.\n");
            }
        }

        public static void prodMaisCaro(Dictionary<string,List<Produto>> loja)
        {
            double maiork = 0, maioru = 0;
            string nomek = "", nomeu = "";
            foreach (KeyValuePair<string, List<Produto>> mostrar in loja)
            {
                foreach (Produto prod in mostrar.Value)
                {
                    if (prod.UnidadeMed == "kg")        // para diferenciar de cada unidade de medida e mostrar o maior dos kilos e o maior dos unitários
                    {
                        if (prod.precoU > maiork)
                        {
                            maiork = prod.precoU;
                            nomek = prod.nomeP;

                        }
                    }
                    else
                    {
                        if (prod.precoU > maioru)
                        {
                            maioru = prod.precoU;
                            nomeu = prod.nomeP;
                        }
                    }
                }
            }
            Console.WriteLine("O produto mais caro da unidade de medida, kilos, é " + nomek + " com um preço de " + maiork + "€.");
            Console.WriteLine("O produto mais caro da unidade de medida, unitária, é " + nomeu + " com um preço de " + maioru + "€.");
        }

        public static void  mediaPrecoArea(Dictionary<string,List<Produto>> loja)
        {
            double media;
            foreach (KeyValuePair<string, List<Produto>> mostrar in loja)
            {
                double soma = 0;
                foreach (Produto prod in mostrar.Value)
                {
                    soma = soma + prod.precoU; 
                }
                media = soma / mostrar.Value.Count() ;
                Console.WriteLine("A média de preços de " + mostrar.Key + " é de: "+ Math.Round(media, 2, MidpointRounding.ToEven) + "€.");     // Usamos o Math.Round para arredondar o número a duas casas decimais
            }
           
        }

        public static void geraMaisReceita(List<Venda> vendas)
        {
            Dictionary<string, double> maisreceita = new Dictionary<string, double>();                              //Criação de um dicionario local
            string produto;                                                                                         //Atribuição do nome produto á chave do dicionario
            double gastocliente;                                                                                    // Valor gasto
            double maior = 0;

            foreach(Venda v in vendas)
            {
                produto = v.nomeP;
                gastocliente = v.precoTot;
                if (maisreceita.ContainsKey(produto))                                                           //Verifica se o dicionario contem a chave
                {
                    maisreceita[produto] = gastocliente + gastocliente ;
                }
                else
                {
                    maisreceita.Add(produto, gastocliente);
                }
            }
            foreach (KeyValuePair<string, double> receita in maisreceita)
            {
                if (receita.Value >= maior)
                {
                    maior = receita.Value;
                }
            }
            foreach (KeyValuePair<string, double> receita in maisreceita)                                   //Se houver produtos com receitas iguais mostra todos
            {
                if (receita.Value == maior)
                {
                    Console.WriteLine("O produto que gera mais receita (" + maior + "€) é " + receita.Key + ".");
                }
            }
        }

        public static void areaMaisvendida(List<Venda> vendas)
        {
            Dictionary<string, double> areavend = new Dictionary<string, double>();                                     //criação de um dicionario local
            string area;                                                                                                //chave = area
            double quant;                                                                                               //Valor  = quantidade vendida
            double maior = -100;

            foreach (Venda v in vendas)                                                                                 //igual ao metodo anterior
            {
                area = v.areavenda;
                quant = v.quantComp;
                if (areavend.ContainsKey(area))
                {
                    areavend[area] = quant + quant;
                }
                else
                {
                    areavend.Add(area, quant);
                }
            }
            foreach (KeyValuePair<string, double> receita in areavend)
            {
                if (receita.Value >= maior)
                {
                    maior = receita.Value;
                }
            }
            foreach (KeyValuePair<string, double> receita in areavend)
            {
                if (receita.Value == maior)
                {
                    Console.WriteLine("A área mais vendida é " + receita.Key + " com um total de " + maior + " produtos.");
                }
            }
        }

        public static void clienteMaisgasto(List<Venda> vendas)
        {
            Dictionary<string, double> clientegastos = new Dictionary<string, double>();                                                // Igual aos anteriores
            string cliente;                                                                                                             //chave = cliente
            double gastos;                                                                                                              // valor = gastos por cliente
            double maior = 0;
            foreach (Venda v in vendas)
            {
                cliente = v.nomeC;
                gastos = v.precoTot;
                if (clientegastos.ContainsKey(cliente))
                {
                    clientegastos[cliente] = gastos + gastos;
                }
                else
                {
                    clientegastos.Add(cliente, gastos);
                }
            }
            foreach (KeyValuePair<string, double> receita in clientegastos)
            {
                if (receita.Value >= maior)
                {
                    maior = receita.Value;
                }
            }
            foreach (KeyValuePair<string, double> receita in clientegastos)
            {
                if (receita.Value == maior)
                {
                    Console.WriteLine("O cliente com mais gastos (" + maior + "€) é " + receita.Key + ".");
                }
            }
        }

        public static bool gravarFicheiroXML(Dictionary<string,List<Produto>>loja, Dictionary<string, List<Cliente>> clientes, List<Venda>vendas,string caminho)
        {
            bool xml = false;
            StreamWriter F = new StreamWriter(caminho);
            try
            {
                F.WriteLine("<Loja_Alimentar>");

            foreach (KeyValuePair<string, List<Cliente>> elem in clientes)
            {
                F.WriteLine("\t<Cidade>" + elem.Key);
                foreach (Cliente p in elem.Value)
                {
                    F.WriteLine("\t\t<Cliente>");

                    F.WriteLine("\t\t\t<ID>" + p.idCliente + "</ID>");
                    F.WriteLine("\t\t\t<Nome>" + p.nomeC + "</Nome>");
                    F.WriteLine("\t\t\t<Data_de_Nascimento>" + p.dataNasci + "</Data_de_Nascimento>");

                    F.WriteLine("\t\t</Cliente>");
                }
                F.WriteLine("\t</Cidade>");
            }

            foreach (KeyValuePair<string, List<Produto>> elem in loja)
            {
                F.WriteLine("\t<Area_do_Produto>" + elem.Key);
                foreach (Produto p in elem.Value)
                {
                    F.WriteLine("\t\t<Produto>");

                    F.WriteLine("\t\t\t<ID>" + p.idProduto + "</ID>");
                    F.WriteLine("\t\t\t<Nome>" + p.nomeP + "</Nome>");
                    F.WriteLine("\t\t\t<Preço_Unitário>" + p.precoU + "</Preço_Unitário>");
                    F.WriteLine("\t\t\t<Unidade_Medida>" + p.UnidadeMed + "</Unidade_Medida>");
                    F.WriteLine("\t\t\t<Quantidade_Disponivel>" + p.quantDis + "</Quantidade_Disponivel>");
                    
                    F.WriteLine("\t\t</Produto>");
                }
                F.WriteLine("\t</Area_do_Produto>");
            }

            
            
                F.WriteLine("\t<Historico_de_Vendas>");
                foreach (Venda p in vendas)
                {
                    F.WriteLine("\t\t<Venda>");
                    F.WriteLine("\t\t\t<ID_do_Cliente>" + p.idCliente + "</ID_do_Cliente>");
                    F.WriteLine("\t\t\t<Nome_do_Cliente>" + p.nomeC + "</Nome_do_Cliente>");
                    F.WriteLine("\t\t\t<Area_do_Produto>" + p.areavenda + "</Area_do_Produto>");
                    F.WriteLine("\t\t\t<Nome_do_Produto>" + p.nomeP + "</Nome_do_Produto>");
                    F.WriteLine("\t\t\t<Preço_Unitário>" + p.precoU + "</Preço_Unitário>");
                    F.WriteLine("\t\t\t<Quantidade_Disponivel>" + p.quantDis + "</Quantidade_Disponivel>");
                    F.WriteLine("\t\t\t<Quantidade_Comprada>" + p.quantComp + "</Quantidade_Comprada>");
                    F.WriteLine("\t\t\t<Preço_Total>" + p.precoTot + "</Preço_Total>");
                    F.WriteLine("\t\t</Venda>");

            }

                F.WriteLine("\t</Historico_de_Vendas>");
            

                F.WriteLine("</Loja_Alimentar>");
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message, "Erro!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            finally
            {
                if (F != null)
                    F.Close();
                xml = true;
            }
            return xml;
        }

        public static void mostrarClientes(Dictionary<string, List<Cliente>> clientes)
        {
            foreach (KeyValuePair<string, List<Cliente>> mostrar in clientes)
            {
                Console.WriteLine(mostrar.Key + ":");
                foreach (Cliente pes in mostrar.Value)
                {

                    Console.WriteLine("\t" + "ID "+ pes.idCliente + " - " + pes.nomeC + " - Data de Nascimento: " + pes.dataNasci);
                }
            }
        }

        public static bool removerCliente(Dictionary<string, List<Cliente>> clientes, string nome)
        {
            bool achou = false;
            foreach (KeyValuePair<string, List<Cliente>> elem in clientes)
            {
                foreach (Cliente pes in elem.Value)
                { 
                    if (pes.nomeC.ToLower() == nome.ToLower())      // procura se o nome introduzido é igual a algum já existente de algum cliente, se for entra no if e elimina esse cliente
                    {
                        elem.Value.RemoveAll(r => r.nomeC.ToLower().Contains(nome));
                        achou = true;
                        break;
                    }
                }
            }
            return achou;
        }

        public static bool removerProduto(Dictionary<string, List<Produto>> loja, string nome)
        {
            bool achou = false;
            foreach (KeyValuePair<string, List<Produto>> elem in loja)
            {
                foreach (Produto prod in elem.Value)
                {
                    if (prod.nomeP.ToLower() == nome.ToLower())     // procura se o nome do produto que é introduzido é igual a algum já existente de algum produto, se for entra no if e elimina esse produto
                    {
                        elem.Value.RemoveAll(r => r.nomeP.ToLower().Contains(nome));
                        achou = true;
                        break;
                    }
                }
            }
            return achou;
        }

        public static bool adicionarStock (Dictionary<string, List<Produto>> loja, string nome)
        {
            double stock;
            bool adicionado = false;
            foreach (KeyValuePair<string, List<Produto>> mostrar in loja)
            {
                foreach (Produto prod in mostrar.Value)
                {
                    if (prod.nomeP.ToLower() == nome.ToLower())
                    {
                        if (prod.UnidadeMed == "und")       // confirmamos se o produto é vendido à unidade ao a kilos, se for unidade entra aqui
                        {
                            do
                            {
                                Console.WriteLine("\nQuanto deseja adicionar ao stock do produto " + prod.nomeP + "? (Máximo 50 unidades, introduza um número inteiro)");
                                stock = Convert.ToDouble(Console.ReadLine());
                                adicionado = true;
                            } while (stock / Math.Truncate(stock) != 1 || stock > 50);          // só deixa adicionar se for introduzido um número inteiro e menor que 50, o máximo são 50 unidades que pode adicionar
                        }                                                                       // se for kilos entra aqui
                        else
                        {
                            do
                            {
                                Console.WriteLine("\nQuanto deseja adicionar ao stock do produto " + prod.nomeP + "? (Máximo 20 kilos)");
                                stock = Convert.ToDouble(Console.ReadLine());
                                adicionado = true;
                            } while (stock > 20);                                               // só deixa adicionar se for introduzido um número menor que 20, o máximo são 20 kilos que pode adicionar
                        }
                        prod.quantDis = prod.quantDis + stock;
                    }
                }
            }
            return adicionado;                                                                 // se o adicionado devolver true, avisa que o stock foi adicionado ao produto, se devolver false avisa que o produto não existe
        }

        public static bool verificarIdProdutoExistente(Dictionary<string, List<Produto>> loja, int id) //metodo para verificar se um id de um produto já existe // validação de entrada do produto
        {                   
            bool valid=false;
            foreach (KeyValuePair<string, List<Produto>> mostrar in loja)
            {
                foreach (Produto prod in mostrar.Value)
                {
                    if(id == prod.idProduto)
                    {
                        valid = true;
                    }
                }
            }
                    return valid;
        }

        public static int incrementaID(Dictionary<string, List<Cliente>> clientes) //metodo para o id do cliente ser feito automaticamente, assim, sempre que é introduzido um novo cliente é o id é automatico
        {
            int id;
            int maior=0;
            foreach(KeyValuePair<string, List<Cliente>> mostar in clientes)
            {
                foreach(Cliente pes in mostar.Value)
                {
                    if (pes.idCliente > maior)
                    {
                        maior = pes.idCliente;
                    }
                }
            }
            id = maior + 1;
            return id;
        }

        public static bool verificarCidade(Dictionary<string, List<Cliente>> clientes, string cidade) //metodo para verificar se uma cidade já existe
        {
            bool valid = false;
            foreach (KeyValuePair<string, List<Cliente>> mostrar in clientes)
            {
                foreach (Cliente pes in mostrar.Value)
                {
                    if (cidade == pes.cidade)
                    {
                        valid = true;
                    }
                }
            }
            return valid;
        }









    }
}

