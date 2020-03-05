using System.IO;
using DevTest;

namespace ConsoleApp
{
    public class CustomStream : LorumIpsumStream
    {
        private readonly string _customString;

        public CustomStream(string customString)
        {
            _customString = customString;
        }

        //Just read normal text
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanRead { get; }
        public override bool CanSeek { get; }
        public override bool CanWrite { get; }
        public override long Length { get; }
        public override long Position { get; set; }
    }

    // class CustomStreamWriter : StreamWriter
    // {
    //     
    // }
}