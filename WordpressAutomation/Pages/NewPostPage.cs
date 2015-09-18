using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WordpressAutomation
{
    public class NewPostPage
    {
        public static void Goto()
        {
            LeftNavigation.Posts.AddNew.Select();            
        }

        public static CreatePostCommand CreatePost(string title)
        {
            return new CreatePostCommand(title);
        }

        public static void GoToNewPost()
        {
            var message = Driver.Instance.FindElement(By.Id("message"));
            var newPostLink = message.FindElements(By.TagName("a"))[0];
            newPostLink.Click();

        }

        public static bool IsInEditMode()
        {
            return Driver.Instance.FindElement(By.Id("insert-media-button")) != null;
        }

        public static string Title
        {
            get
            {
                var title = Driver.Instance.FindElement(By.Id("title"));
                if (title != null)
                {
                    return title.GetAttribute("value");
                } else
                {
                    return String.Empty;
                }
            }
        }

       
    }

        public class CreatePostCommand
        {
            private readonly string title;
            private string body;

            

            public CreatePostCommand(string title)
            {
                this.title = title;
            }

            public CreatePostCommand WithBody(string body)
            {
                this.body = body;
                return this;
            }
                        
            public void Publish()
            {
                var titleInput = Driver.Instance.FindElement(By.Id("title"));
                titleInput.SendKeys(title);

                var switchFrame = Driver.Instance.SwitchTo();
                switchFrame.Frame("content_ifr");
                var switchActive = switchFrame.ActiveElement();
                switchActive.SendKeys(body);
                switchFrame.DefaultContent();

                Driver.Wait(TimeSpan.FromSeconds(5));

                var publishButton = Driver.Instance.FindElement(By.Id("publish"));
                publishButton.Click();
            }
        }
}
