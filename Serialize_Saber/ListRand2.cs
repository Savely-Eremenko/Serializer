using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer_Saber
{
    class ListRand2
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;
        public void Serialize(FileStream s)
        {

            using (StreamWriter sw = new StreamWriter(s))
            {
                int randNum;

                sw.WriteLine("SaberSerializer");
                
                foreach (ListNode ln in this.GetNote())
                {
                    randNum = 0;
                    foreach(ListNode l in this.GetNote())
                    {
                        if (ln.Rand == l)
                            break;
                        randNum++;
                    }
                    sw.WriteLine($"{ln.Data}\n{randNum.ToString()}");
                }
            }
        }

        public void Deserialize(FileStream s)
        {
            List<int> listRandNum = new List<int>();
            ListNode current = new ListNode();
            Count = 0;
            Head = current;
            string line;
            int iterator=0;

            try
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    if (sr.ReadLine() != "SaberSerializer")
                        throw new Exception("Файл создан не данной программой! " +
                            "Программа поддерживает исключительно файлы, созданные с её помощью.");

                    while ((line = sr.ReadLine()) != null)
                    {
                        current.Data = line;
                        listRandNum.Add(Convert.ToInt32(sr.ReadLine()));
                        ListNode next = new ListNode() { Prev = current };
                        current.Next = next;
                        current = next;
                        Count++;
                    }

                    Tail = current.Prev;
                    Tail.Next = null;

                    foreach(ListNode ln in this.GetNote())
                    {
                        ListNode rand=Head;

                        for (int i = 0; i < listRandNum[iterator]; i++)
                            rand = rand.Next;

                        ln.Rand = rand;
                        iterator++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка в ходе восстановления файла: {e.Message}");
            }
        }

        public IEnumerable GetNote()
        {
            ListNode current = Head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }

        }
    }
}
