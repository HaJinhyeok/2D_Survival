public class Protocols
{
    public class Packets
    {
        public class common
        {
            public int cmd;
        }
        public class req_scores : common
        {
            public string id;
            public int score;
        }
        public class req_login:common
        {
            public string id;
            public string password;
        }

        public class res_message : common
        {
            public string message;
        }

        public class user
        {
            public string id;
            public string password;
            public int score;
        }

        public class res_scores_top3 : res_message
        {
            public user[] result;
        }

        public class res_info : res_message
        {
            public user result;
        }
    }
}