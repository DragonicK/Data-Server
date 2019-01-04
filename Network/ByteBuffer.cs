using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Server.Network {
    public sealed class ByteBuffer {
        List<byte> buffer;
        int readpos = 0;

        public ByteBuffer() {
            buffer = new List<byte>();
        }

        public ByteBuffer(byte[] arr) {
            buffer = new List<byte>();
            buffer.AddRange(arr);
        }

        private int ReadPosition() {
            return readpos;
        }

        public byte[] ToArray() {
            return buffer.ToArray();
        }

        public int Length() {
            return buffer.Count - readpos;
        }

        public int Count() {
            return buffer.Count;
        }

        public void Clear() {
            buffer.Clear();
            readpos = 0;
        }

        public void Write(byte[] value) {
            buffer.AddRange(value);
        }

        public void Write(byte value) {
            buffer.Add(value);
        }

        public void Write(short value) {
            buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void Write(int value) {
            buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void Write(long value) {
            buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void Write(string value) {
            buffer.AddRange(BitConverter.GetBytes(value.Length));
            buffer.AddRange(Encoding.ASCII.GetBytes(value));
        }

        public byte[] ReadBytes(int length, bool peek = true) {
            var values = buffer.GetRange(readpos, length);

            if (peek) {
                readpos += length;
            }

            return values.ToArray();
        }
        
        public byte ReadByte(bool peek = true) {
            var value = buffer[readpos];

            if (peek) {
                readpos += 1;
            }

            return value;
        }

        public short ReadInt16(bool peek = true) {
            var value = BitConverter.ToInt16(ToArray(), readpos);

            if (peek) {
                readpos += 2;
            }

            return value;
        }

        public int ReadInt32(bool peek = true) {
            var value = BitConverter.ToInt32(buffer.ToArray(), readpos);

            if (peek) {
                readpos += 4;
            }

            return value;
        }    

        public string ReadString(bool peek = true) {
            var length = ReadInt32();
            var text = Encoding.ASCII.GetString(ToArray(), readpos, length);

            if (peek) {
                readpos += text.Length;
            }

            return text;
        }
    }
}