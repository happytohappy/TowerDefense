using Firebase.Database;

using UnityEngine;

public class UIWindowIntro : UIWindowBase
{
    // ***** 파베 테스트
    public class User
    {
        public string username;
        public string email;
        public User(string username, string email)
        {
            this.username = username;
            this.email = email;
        }
    }

    private DatabaseReference reference;
    // *****

    public override void Awake()
    {
        Window_ID = WindowID.UIWindowIntro;
        Window_Mode = WindowMode.WindowClose;

        base.Awake();
    }

    public override void OpenUI(WindowParam wp)
    {
        base.OpenUI(wp);
    }

    // ***** 파베 테스트
    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    // *****

    public void OnClickMain()
    {
        // ***** 파베 테스트
        writeUser("Info", "jinijini", "jini@gmail.com");
        roadUser("Info");
        // *****

        Managers.UI.Clear();

        LoadingParam param = new LoadingParam();
        param.SceneIndex = 1;
        param.NextWindow = WindowID.UIWindowMain;

        Managers.UI.OpenWindow(WindowID.UIWindowLoading, param);
    }

    // ***** 파베 테스트
    private void writeUser(string userId, string name, string email)
    {
        User user = new User(name, email);
        string json = JsonUtility.ToJson(user);
        reference.Child(userId).SetRawJsonValueAsync(json);
    }

    private void roadUser(string userId)
    {
        reference.Child(userId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
                Debug.Log("에러!");
            else if (task.IsCompleted)
            {
                var dataSnapshot = task.Result;

                string dataString = "";
                foreach (var data in dataSnapshot.Children)
                {
                    dataString += data.Key + " " + data.Value + "\n";
                }

                Debug.Log(dataString);
            }
        });
    }
    // *****
}