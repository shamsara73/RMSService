using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMSModel.Tools
{
    public enum MessageType
    {
        Success,
        Warning,
        Error,
        Info
    }
    public enum MessageTypeClass
    {
        Success,
        Warning,
        Danger,
        Info
    }
    public class MessageDefinition
    {
        public string Message { get; set; }
        private string _title { get; set; }
        public string Title {
            get {
                return _title;
            }
            set{
                _title = value;
                this.Message = _title;
            } 
        }

        private MessageType _type { get; set; }
        public MessageType Type { 
            get {
                return _type;
            } 
            set {
                switch (value)
                {
                    case MessageType.Success:
                        this.TypeClass = MessageTypeClass.Success;
                        break;
                    case MessageType.Warning:
                        this.TypeClass = MessageTypeClass.Warning;
                        break;
                    case MessageType.Error:
                        this.TypeClass = MessageTypeClass.Danger;

                        break;
                    case MessageType.Info:
                        this.TypeClass = MessageTypeClass.Info;

                        break;
                    default:
                        break;
                }
                _type = value;
            } 
        }
        public MessageTypeClass TypeClass { get; set; }
    }
}
