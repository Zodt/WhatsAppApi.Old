using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace WhatsAppApi.Operation.Messages
{
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class ListMessage : IEnumerable<WhatsAppMessageProperties>
    {
        private List<WhatsAppMessageProperties> Messages { get; set; }

        public int Count { get; }
        public bool IsReadOnly { get; } = false;
        public BigInteger LastMessageNumber { get; set; }

        ~ListMessage() => Messages = default;
        public ListMessage(List<WhatsAppMessageProperties> messages)
        {
            Messages = messages;
            Count = messages.Count;
            LastMessageNumber = messages.OrderByDescending(x => x.MessageNumber ?? default).Select(x => x.MessageNumber ?? default).FirstOrDefault();
        }

        public static implicit operator List<WhatsAppMessageProperties>(ListMessage list) => list.Messages;
        public static explicit operator ListMessage(List<WhatsAppMessageProperties> list) => new ListMessage(list);


        IEnumerator IEnumerable.GetEnumerator() => Messages.GetEnumerator();
        IEnumerator<WhatsAppMessageProperties> IEnumerable<WhatsAppMessageProperties>.GetEnumerator() => Messages.GetEnumerator();
        public object this[BigInteger index]
        {
            get => Messages.FirstOrDefault(x => x.MessageNumber == index);
            set
            {
                if (Messages.FirstOrDefault(x => x.MessageNumber == index) != null)
                    Messages[Messages.IndexOf(Messages.FirstOrDefault(x => x.MessageNumber == index))] =
                        (WhatsAppMessageProperties)value ?? throw new ArgumentNullException(nameof(value));
            }
        }

    }
}