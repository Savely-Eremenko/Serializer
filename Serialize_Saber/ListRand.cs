using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer_Saber
{
    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;
        public void Serialize(FileStream s)
        {
            List<ListNode> list = new List<ListNode>();
            ListNode current = new ListNode();
            current = Head;

            while (current != null)
            {
                list.Add(current);
                current = current.Next;
            }

            using (StreamWriter sw = new StreamWriter(s))
            {
                sw.WriteLine("SaberSerializer");
                foreach (ListNode ln in list)
                    sw.WriteLine($"{ln.Data}\n{list.IndexOf(ln.Rand).ToString()}");
            }
        }

        public void Deserialize(FileStream s)
        {
            List<ListNode> list = new List<ListNode>();
            ListNode current = new ListNode();
            Count = 0;
            Head = current;
            string line;

            try
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    if (sr.ReadLine() != "SaberSerializer")
                        throw new Exception("Файл создан не данной программой! " +
                            "Программа поддерживает исключительно файлы, созданные с её помощью.");

                    while ((line = sr.ReadLine()) != null)
                    {
                        current.Data = $"{line}\n{sr.ReadLine()}";
                        ListNode next = new ListNode() { Prev = current };
                        current.Next = next;
                        list.Add(current);
                        current = next;
                        Count++;
                    }

                    Tail = current.Prev;
                    Tail.Next = null;

                    foreach (ListNode ln in list)
                    {
                        ln.Rand = list[Convert.ToInt32(ln.Data.Split('\n')[1])];
                        ln.Data = ln.Data.Split('\n')[0];
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Ошибка в ходе восстановления файла: {e.Message}");
            }
        }
    }
}
