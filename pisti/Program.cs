using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pisti
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("İsminiz: ");
            string player1 = Console.ReadLine();
            Console.Write("Soy isminiz: ");
            string player2 = Console.ReadLine();
            Console.Write("Doğum gününüz: ");
            int birthDay = Convert.ToInt32(Console.ReadLine());

            
            
            /*          OYUNUN KURALLARI
             * 
             * Aynı sayıyı atan kazanmış sayılır ve masadaki kartları hanesine ekler.
             * 
             * Eğer masada kart yokken kart atılıp üzerine rakip de aynı kartı atarsa pişti olur 10 puan kazanılır.
             * 
             * Kasada kart bitince oyun biter. Oyun sonunda masada halen kart kaldıysa bir önceki eli alan oyuncunun hanesine masadaki kartlar eklenir.
             * 
             */

            /*          PUANLAMA
             * 
             * Pişti                                    ->      10 puan
             * Sinek İki                                ->      2 Puan
             * Bir'ler                                  ->      1 Puan
             * Karo On                                  ->      3 Puan
             * Oyun sonunda elinde fazla kart olan      ->      3 Puan
             * 
             */


            Console.WriteLine("***Kartlar hazırlanıyor***");
            List<string> deste = desteOlustur();
            deste = desteKaristir(deste,birthDay);

            // 1. oyuncu 
            List<string> player1GainedCards = new List<string>();  
            List<string> player1CardsInHand = new List<string>();
            List<string> player1PlayedCard = new List<string>();
            int player1PistiCounter=0;


            //2. oyuncu listeleri
            List<string> player2GainedCards = new List<string>();
            List<string> player2CardsInHand = new List<string>();
            List<string> player2PlayedCard = new List<string>();
            int player2PistiCounter=0;


            //Ortadaki kart listesi
            List<string> cardsInGame = new List<string>();


            string whoLastWin="";

            for (int i = 0; i < 4; i++)
            {
                cardsInGame.Add(deste[0]);
                deste.RemoveAt(0);
            }

            for (int i = 0; i < 4; i++)
            {
                player1CardsInHand.Add(deste[0]);
                deste.RemoveAt(0);
            }

            for (int i = 0; i < 4; i++)
            {
                player2CardsInHand.Add(deste[0]);
                deste.RemoveAt(0);
            }

            Random rand = new Random();
            int queueCounter = rand.Next();
            int choose;
            string[] player1CardName;
            string[] player2CardName;
            string[] cache;
            while (true)
            {
                while (true)
                {
                    if (cardsInGame.Count == 0)
                    {
                        Console.WriteLine("Masada Kart Yok.");
                    }
                    else
                    {
                        Console.WriteLine("Masadaki kart: " + cardsInGame[cardsInGame.Count - 1]);
                    }

                    if (queueCounter % 2 == 0)
                    {
                        queueCounter++;
                        Console.WriteLine("Sıradaki Oyuncu: {0}", player1);
                        for (int i = 0; i < player1CardsInHand.Count; i++)
                        {
                            Console.WriteLine("{0}-) {1}", (i + 1), player1CardsInHand[i]);
                        }
                        Console.WriteLine("Lütfen atacağınız kartın sayısını giriniz: ");
                        choose = Convert.ToInt32(Console.ReadLine());
                        player1PlayedCard.Add(player1CardsInHand[choose-1]);
                        player1CardName = player1CardsInHand[choose - 1].Split(' ');
                        try
                        {
                            cache = cardsInGame[cardsInGame.Count - 1].Split(' ');

                            if (player1CardName[1] == cache[1])
                            {
                                if (cardsInGame.Count == 1)
                                {
                                    player1PistiCounter++;
                                }
                                cardsInGame.Add(player1CardsInHand[choose - 1]);
                                player1CardsInHand.RemoveAt(choose - 1);
                                for (int i = 0; i < cardsInGame.Count; i++)
                                {
                                    player1GainedCards.Add(cardsInGame[i]);
                                }
                                cardsInGame.Clear();
                                whoLastWin = "p1";
                            }
                            else
                            {
                                cardsInGame.Add((player1CardsInHand[choose - 1]));
                                player1CardsInHand.RemoveAt(choose - 1);
                            }
                        }
                        catch(Exception ex)
                        {
                            cardsInGame.Add((player1CardsInHand[choose - 1]));
                            player1CardsInHand.RemoveAt(choose - 1);
                        }
                    }
                    else
                    {
                        queueCounter++;
                        Console.WriteLine("Sıradaki Oyuncu: {0}", player2);
                        for (int i = 0; i < player2CardsInHand.Count; i++)
                        {
                            Console.WriteLine("{0}-) {1}", (i + 1), player2CardsInHand[i]);
                        }
                        Console.WriteLine("Lütfen atacağınız kartın sayısını giriniz: ");
                        choose = Convert.ToInt32(Console.ReadLine());
                        player2PlayedCard.Add(player2CardsInHand[choose - 1]);
                        player2CardName = player2CardsInHand[choose - 1].Split(' ');
                        try
                        {
                            cache = cardsInGame[cardsInGame.Count - 1].Split(' ');


                            if (player2CardName[1] == cache[1])
                            {
                                if (cardsInGame.Count == 1)
                                {
                                    player2PistiCounter++;
                                }
                                cardsInGame.Add(player2CardsInHand[choose - 1]);
                                player2CardsInHand.RemoveAt(choose - 1);
                                for (int i = 0; i < cardsInGame.Count; i++)
                                {
                                    player1GainedCards.Add(cardsInGame[i]);
                                }
                                cardsInGame.Clear();
                                whoLastWin = "p2";
                            }
                            else
                            {
                                cardsInGame.Add((player2CardsInHand[choose - 1]));
                                player2CardsInHand.RemoveAt(choose - 1);
                            }
                        }
                        catch(Exception ex)
                        {
                            cardsInGame.Add((player2CardsInHand[choose - 1]));
                            player2CardsInHand.RemoveAt(choose - 1);
                        }
                    }
                    if (player1CardsInHand.Count == 0 && player2CardsInHand.Count == 0)
                        break;
                }

                if (deste.Count == 0)
                    break;
                for (int i = 0; i < 4; i++)
                {
                    player1CardsInHand.Add(deste[0]);
                    deste.RemoveAt(0);
                }

                for (int i = 0; i < 4; i++)
                {
                    player2CardsInHand.Add(deste[0]);
                    deste.RemoveAt(0);
                }
            }

            if (cardsInGame.Count != 0)
            {
                if (whoLastWin == "p1")
                {
                    for(int i = 0; i < cardsInGame.Count; i++)
                    {
                        player1GainedCards.Add(cardsInGame[i]);
                    }
                    cardsInGame.Clear();
                }
                else if (whoLastWin == "p2")
                {
                    for (int i = 0; i < cardsInGame.Count; i++)
                    {
                        player2GainedCards.Add(cardsInGame[i]);
                    }
                    cardsInGame.Clear();
                }
            }



            Console.WriteLine("***OYUN BİTTİ***\nPuanlarınız Hesaplanıyor...");
            int player1Point = 0;
            foreach(string player1card in player1GainedCards)
            {
                if (player1card == "Sinek İki")
                    player1Point += 2;

                else if (player1card == "Kupa Bir" || player1card == "Maça Bir" || player1card == "Karo Bir" || player1card == "Sinek Bir")
                    player1Point++;

                else if (player1card == "Karo On")
                    player1Point += 3;
            }

            int player2Point = 0;
            foreach (string player2card in player2GainedCards)
            {
                if (player2card == "Sinek İki")
                    player2Point += 2;

                else if (player2card == "Kupa Bir" || player2card == "Maça Bir" || player2card == "Karo Bir" || player2card == "Sinek Bir")
                    player2Point++;

                else if (player2card == "Karo On")
                    player2Point += 3;
            }

            if (player1GainedCards.Count > player2GainedCards.Count)
                player1Point += 3;
            else if (player1GainedCards.Count < player2GainedCards.Count)
                player2Point += 3;

            player1Point += (player1PistiCounter * 10); 
            player2Point += (player2PistiCounter * 10);

            if (player1Point> player2Point)
                Console.WriteLine("*********Kazanan {0}********\nPuanı: {1}",player1, player1Point);
            
            else if (player1Point < player2Point)
                Console.WriteLine("*********Kazanan {0}********\nPuanı: {1}",player2, player2Point);
            
            else
                Console.WriteLine("Oyun Berabere!");

            Console.WriteLine("{0} oyuncusunun oynadığı kartlar:",player1);
            foreach(string cards in player1PlayedCard)
            {
                Console.Write("{0}\t",cards);
            }
            Console.WriteLine();
            Console.WriteLine("{0} oyuncusunun oynadığı kartlar:",player2);
            foreach (string cards in player2PlayedCard)
            {
                Console.Write("{0}\t", cards);
            }



            Console.ReadKey();
        }

        static List<string> desteOlustur()
        {
            List<string> dest = new List<string>();
            String[] sayilar = {"Bir", "İki","Üç","Dört","Beş","Altı","Yedi","Sekiz","Dokuz","On","Vale","Kız","Papa"};
            String[] turleri = {"Kupa","Maça","Karo","Sinek"};
            string card;
            for (int i = 0; i < turleri.Length; i++)
            {
                for (int j = 0; j < sayilar.Length; j++)
                {
                    card = turleri[i]+" "+sayilar[j];
                    dest.Add(card);
                }
            }
            return dest;
        }


        static List<string> desteKaristir(List<string> destee, int yas)
        {
            List<string> destekarma = new List<string>();
            var random = new Random();
            for (int i = 0; i < yas; i++)
            {
                destekarma = destee.OrderBy(item => random.Next()).ToList();
            }
            return destekarma;
        }
    }
}