using System;
using System.Collections.Concurrent;
using DevTest;

namespace ConsoleApp
{
    /// <summary>
    /// This stream object mimics the LorumIpsumStream
    /// Main difference is it reads a given string
    /// </summary>
    public class CustomStream : LorumIpsumStream
    {
        private readonly BlockingCollection<byte> _byteQueue = new BlockingCollection<byte>();
        private readonly int _requiredTotalKB;
        private readonly string _rawText;
        private long ReadTotalKB { get; set; }

        public CustomStream(int requiredTotalKB, string customString)
        {
            _requiredTotalKB = requiredTotalKB;
            _rawText = customString;
            PopulateQueue();
        }

        //Just read normal text
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (ReadTotalKB > _requiredTotalKB)
                return 0;
            
            int num = Read(buffer, count);
            ReadTotalKB += num / 1024;
            return num;
        }

        public override bool CanRead { get; }
        public override bool CanSeek { get; }
        public override bool CanWrite { get; }
        public override long Length { get; }
        public override long Position { get; set; }

        private static byte[] GetBytes(string str)
        {
            byte[] numArray = new byte[str.Length * 2];
            Buffer.BlockCopy(str.ToCharArray(), 0, numArray, 0, numArray.Length);
            return numArray;
        }

        private byte[] GetBuffer()
        {
            return GetBytes(_rawText);
        }

        private void PopulateQueue()
        {
            foreach (byte num in GetBuffer())
                _byteQueue.Add(num);
        }

        internal int Count
        {
            get
            {
                return _byteQueue.Count;
            }
        }

        private int Read(byte[] buffer, int count)
        {
            int num1 = 0;
            for (int index = 0; index < count; ++index)
            {
                if (_byteQueue.Count <= 0)
                    PopulateQueue();
                
                if (_byteQueue.TryTake(out var num2))
                {
                    buffer[index] = num2;
                    ++num1;
                }
                else
                    break;
            }
            return num1;
        }
    }
}