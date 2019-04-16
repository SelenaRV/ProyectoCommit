using AccenCart.Interfaces;
using AccenCart.iOS.Services;
using CoreNFC;
using Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(iOSNFCReader))]
namespace AccenCart.iOS.Services
{
    public class iOSNFCReader : NSObject, INFCReader, INFCNdefReaderSessionDelegate
    {
        public event EventHandler<string> TagReaded;
        public event EventHandler<string> ReadingCancelled;
        private object objectLock = new Object();
        List<NFCNdefMessage> DetectedMessages = new List<NFCNdefMessage> { };
        NFCNdefReaderSession Session;
        private bool nfcEnabled;

        public iOSNFCReader()
        {
            IsNFCCapable();
        }

        public Task<bool> IsNFCCapable()
        {
            nfcEnabled = NFCNdefReaderSession.ReadingAvailable;

            return Task.FromResult<bool>(nfcEnabled);
        }

        public Task Start()
        {

            if (nfcEnabled)
            {
                Session = new NFCNdefReaderSession(this, null, true);
                if (Session != null)
                {
                    Session.BeginSession();
                }
            }

            return Task.CompletedTask;
        }

        public Task Stop()
        {
            Session.InvalidateSession();

            return Task.CompletedTask;
        }

        public void DidInvalidate(NFCNdefReaderSession session, NSError error)
        {
            var readerError = (NFCReaderError)(long)error.Code;

            if (readerError != NFCReaderError.ReaderSessionInvalidationErrorFirstNDEFTagRead &&
                readerError != NFCReaderError.ReaderSessionInvalidationErrorUserCanceled)
            {
                ReadingCancelled?.Invoke(this, error.LocalizedDescription);
            }
        }

        public void DidDetect(NFCNdefReaderSession session, NFCNdefMessage[] messages)
        {
            foreach (NFCNdefMessage msg in messages)
            {
                DetectedMessages.Add(msg);

                if(msg.Records != null && msg.Records.Length > 0)
                {
                    var payload = GetPayload(msg.Records[0]);

                    TagReaded?.Invoke(this, payload);
                }
            }
        }

       

        private string GetPayload(NFCNdefPayload payload)
        {
            string result;

            switch (payload.TypeNameFormat)
            {
                case NFCTypeNameFormat.NFCWellKnown:
                    var type = new NSString(payload.Type, NSStringEncoding.UTF8);
                    if (type != null)
                    {
                        result = $"NFC Well Known type: {type}";
                    }
                    else
                    {
                        result = "Invalid data";
                    }
                    break;
                case NFCTypeNameFormat.AbsoluteUri:
                    var text = new NSString(payload.Payload, NSStringEncoding.UTF8);
                    if (text != null)
                    {
                        result = text;
                    }
                    else
                    {
                        result = "Invalid data";
                    }
                    break;
                case NFCTypeNameFormat.Media:
                    var mediaType = new NSString(payload.Type, NSStringEncoding.UTF8);
                    if (mediaType != null)
                    {
                        result = $"Media type: {mediaType}";
                    }
                    else
                    {
                        result = "Invalid data";
                    }
                    break;
                case NFCTypeNameFormat.NFCExternal:
                    result = "NFC External type";
                    break;
                case NFCTypeNameFormat.Unknown:
                    result = "Unknown type";
                    break;
                case NFCTypeNameFormat.Unchanged:
                    result = "Unchanged type";
                    break;
                default:
                    result = "Invalid data";
                    break;
            }

            return result;
        }

        event EventHandler<string> INFCReader.TagReaded
        {
            add
            {
                lock (objectLock)
                {
                    TagReaded += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    TagReaded -= value;
                }
            }
        }

        event EventHandler<string> INFCReader.ReadingCancelled
        {
            add
            {
                lock (objectLock)
                {
                    ReadingCancelled += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    ReadingCancelled -= value;
                }
            }
        }
    }
}