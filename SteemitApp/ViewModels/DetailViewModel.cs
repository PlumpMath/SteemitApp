﻿using System;
using System.Text.RegularExpressions;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace SteemitApp.Core.ViewModels
{
    public class DetailViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigation;

        public DetailViewModel(IMvxNavigationService Navigation, PostPresentation Post)
        {
            navigation = Navigation;
            this.Post = Post;
        }

        public override System.Threading.Tasks.Task Initialize()
        {
            if (post.Body.Contains("<p>")) 
            {
                Regex expression = new Regex(@"(http)?s?:?(\/\/[^""']*\.(?:png|jpg|jpeg|gif|png|svg))");
                var matches = expression.Matches(post.Body);    

                foreach (var match in matches)
                {
                    try
                    {
                        string img = match.ToString();
                        post.Body = post.Body.Replace(img, $"<img src=\"{img}\" />");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            string cssStyle = "<style type=\"text/css\">img { width: 100%; }</style>";
            post.Body = cssStyle + post.Body;

            return base.Initialize();
        }

        private PostPresentation post;
        public PostPresentation Post
        {
            get { return post; }
            set { SetProperty(ref post, value); }
        }

        public IMvxCommand RenderedCommand => new MvxCommand<float>(Rendered);

        private void Rendered(float Height) 
        {
            WebViewContentHeight = Height;
        }

        private float webViewContentHeight;
        public float WebViewContentHeight
        {
            get { return webViewContentHeight; }
            set { SetProperty(ref webViewContentHeight, value); }
        }

    }
}
