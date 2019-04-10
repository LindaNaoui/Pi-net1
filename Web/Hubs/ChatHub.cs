using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;
using Microsoft.AspNet.SignalR;
using PI.Domain;
using SpeechLib;
using chatfinal.Models;

namespace Web.Hubs
{
    public class ChatHub : Hub
    {
        static List<UseInChatViewModel> ConnectedUsers = new List<UseInChatViewModel>();
        static List<sentmessage> CurrentMessage = new List<sentmessage>();
        public void Send(string name, string message)
        {

            Clients.All.addNewMessageToPage(name, message);
            SpVoice s = new SpVoice();
            s.Speak(name + " says " + message);
        }
        public void SendMessage(string message)
        {
            
        }
    
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;
            // convert to int
            int idInt = 0;
            Int32.TryParse(id, out idInt);

            if (ConnectedUsers.Count(x => x.id == idInt) == 0)
            {
                string UserImg = "https://res.cloudinary.com/doctolib/image/upload/w_200,h_200,c_fill,g_face/fsglm3zktrh1yceabyfw.jpg";
                DateTime logintime = DateTime.Now;
                ConnectedUsers.Add(new UseInChatViewModel { id = idInt, username = userName, UrlPhoto = UserImg, lastLogin = logintime});
                // send to caller
                Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage);

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName, UserImg, logintime);
            }
        }

        public void SendMessageToAll(string userName, string message, DateTime time)
        {
            string UserImg = "https://res.cloudinary.com/doctolib/image/upload/w_200,h_200,c_fill,g_face/fsglm3zktrh1yceabyfw.jpg";
            // store last 100 messages in cache
            AddMessageinCache(userName, message, time);

            // Broad cast message
            Clients.All.messageReceived(userName, message, time, UserImg);
            SpVoice s = new SpVoice();
            s.Speak(userName + " says " + message);

        }

        private void AddMessageinCache(string userName, string message, DateTime time)
        {
            var id = Context.ConnectionId;
            // convert to int
            int idInt = 0;
            Int32.TryParse(id, out idInt);
            CurrentMessage.Add(new sentmessage { sender_id= idInt , content = message, dateCreated = time });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);

        }

        // Clear Chat History
        public void clearTimeout()
        {
            CurrentMessage.Clear();
        }

       

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var id = Context.ConnectionId;
            // convert to int
            int idInt = 0;
            Int32.TryParse(id, out idInt);
            var item = ConnectedUsers.FirstOrDefault(x => x.id == idInt);
            if (item != null)
            {
                ConnectedUsers.Remove(item);

                var id1 = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.username);

            }
            return base.OnDisconnected(stopCalled);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {
            string fromUserId = Context.ConnectionId;
            // convert to int
            int fromUserInt = 0;
            Int32.TryParse(fromUserId, out fromUserInt);

            int toUserInt = 0;
            Int32.TryParse(toUserId, out toUserInt);


            var toUser = ConnectedUsers.FirstOrDefault(x => x.id == toUserInt);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.id == fromUserInt);

            if (toUser != null && fromUser != null)
            {
                string CurrentDateTime = DateTime.Now.ToString();
                string UserImg = "https://res.cloudinary.com/doctolib/image/upload/w_200,h_200,c_fill,g_face/fsglm3zktrh1yceabyfw.jpg";
                // send to 
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.username, message, fromUser.UrlPhoto, CurrentDateTime);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, toUser.username, message, fromUser.UrlPhoto, CurrentDateTime);
                SpVoice s = new SpVoice();
                s.Speak(toUser.username + " says " + message);
            }

        }
    }

}
