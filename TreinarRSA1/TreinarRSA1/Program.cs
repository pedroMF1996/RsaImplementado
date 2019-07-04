using System;
using TreinarRSA1.Model;

namespace TreinarRSA1
{
    class Program
    {
        private static RsaClass _RsaClass_rsaClass;
        static void Main(string[] args)
        {
            try
            {
                int? keySize = null;
                Console.Write("Escreva o tamanho da chave: \nTamnhos possiveis: \n\t2048 >= keySize >= 512\n");
                int.TryParse(Console.ReadLine(), out int recebe);
                keySize = recebe;

                SetaProvedor(keySize);
                MostraCriptografado();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            finally
            {
                _RsaClass_rsaClass?.Dispose();
            }
        }

        private static void MostraCriptografado()
        {
            float inicio = DateTime.Now.Millisecond;

            string str_mensagem = null;

            Console.Write("Escreva a mensagem a ser criptografada: ");
            str_mensagem = Console.ReadLine();

            string str_mensagemCriptografada = _RsaClass_rsaClass.Encript(str_mensagem);

            Console.WriteLine($"\n{str_mensagemCriptografada}");

            float tempo = DateTime.Now.Millisecond - inicio;
            RsaClass.MostraTempoDeExecucao(tempo);

            MostraDescriptografado(str_mensagemCriptografada);
        }

        private static void MostraDescriptografado(string str_mensagemCriptografada)
        {
            float inicio = DateTime.Now.Millisecond;

            string str_mensagemDescriptografada = _RsaClass_rsaClass.Decript(str_mensagemCriptografada);

            Console.WriteLine($"\n{str_mensagemDescriptografada}");

            float tempo = DateTime.Now.Millisecond - inicio;
            RsaClass.MostraTempoDeExecucao(tempo);
        }

        private static void SetaProvedor(int? keySize)
        {
            float inicio = DateTime.Now.Millisecond;

            _RsaClass_rsaClass = new RsaClass(keySize: keySize.Value);
            float tempo = DateTime.Now.Millisecond - inicio;
            RsaClass.MostraTempoDeExecucao(tempo);
        }
    }
}
