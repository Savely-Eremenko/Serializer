using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer_Saber
{
    class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            int nodeCount = 5;

            ListNode head = new ListNode();
            ListNode tail = new ListNode();
            ListNode current = new ListNode();

            head.Data = rand.Next(0, 100).ToString();

            tail = head;

            for (int i = 1; i < nodeCount; i++)
                tail = AddNode(tail);

            current = head;

            for (int i = 0; i < nodeCount; i++)
            {
                current.Rand = RandomNode(head, nodeCount);
                current = current.Next;
            }

            ListRand first = new ListRand
            {
                Head = head,
                Tail = tail,
                Count = nodeCount
            };


            using (FileStream fs = new FileStream("sourse.dat", FileMode.Create))
                first.Serialize(fs);

            ListRand second = new ListRand();

            try
            {
                using (FileStream fs = new FileStream("sourse.dat", FileMode.Open))
                    second.Deserialize(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                Environment.Exit(0);
            }
            

            if (second.Tail.Data == first.Tail.Data) Console.WriteLine("Data are equal");
            if (second.Tail.Rand.Data == first.Tail.Rand.Data) Console.WriteLine("Random elements are equal");
            Console.Read();
        }

        


        static ListNode AddNode(ListNode prev)
        {
            ListNode result = new ListNode
            {
                Prev = prev,
                Next = null,
                Data = rand.Next(0, 100).ToString()
            };
            prev.Next = result;
            return result;
        }


        static ListNode RandomNode(ListNode _head, int _length)
        {
            int k = rand.Next(0, _length);
            int i = 0;
            ListNode result = _head;
            while (i < k)
            {
                result = result.Next;
                i++;
            }
            return result;
        }
    }
}
