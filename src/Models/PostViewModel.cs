using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Miniblog.Services.Models;
using NUglify.Helpers;

namespace Miniblog.Core.Models
{
    public class PostViewModel
    {
        public PostViewModel()
        {}

        public PostViewModel(Post post)
        {
            Categories = post.Categories;
            Comments = post.Comments.Select(x => new CommentViewModel(x)).ToList();
            Content = post.Content;
            Excerpt = post.Excerpt;
            Id = post.ID;
            IsPublished = post.IsPublished;
            LastModified = post.LastModified;
            PubDate = post.PubDate;
            Slug = post.Slug;
            Title = post.Title;
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Excerpt { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PubDate { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsPublished { get; set; }

        public IList<string> Categories { get; set; } = new List<string>();

        public IList<CommentViewModel> Comments { get; } = new List<CommentViewModel>();

        public string GetLink()
        {
            return $"/blog/{Slug}/";
        }

        public string GetEncodedLink()
        {
            return $"/blog/{System.Net.WebUtility.UrlEncode(Slug)}/";
        }

        public bool AreCommentsOpen(int commentsCloseAfterDays)
        {
            return PubDate.AddDays(commentsCloseAfterDays) >= DateTime.UtcNow;
        }

        public string RenderContent()
        {
            var result = Content;
            if (!string.IsNullOrEmpty(result))
            {
                // Set up lazy loading of images/iframes
                result = result.Replace(" src=\"", " src=\"data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==\" data-src=\"");

                // Youtube content embedded using this syntax: [youtube:xyzAbc123]
                var video = "<div class=\"video\"><iframe width=\"560\" height=\"315\" title=\"YouTube embed\" src=\"about:blank\" data-src=\"https://www.youtube-nocookie.com/embed/{0}?modestbranding=1&amp;hd=1&amp;rel=0&amp;theme=light\" allowfullscreen></iframe></div>";
                result = Regex.Replace(result, @"\[youtube:(.*?)\]", m => string.Format(video, m.Groups[1].Value));
            }
            return result;
        }

        public Post ToPost()
        {
            var post = new Post
            {
                Excerpt = Excerpt,
                Categories = Categories,
                Title = Title,
                LastModified = LastModified,
                Content = Content,
                Slug = Slug,
                ID = Id,
                PubDate = PubDate,
                IsPublished = IsPublished
            };

            Comments.ForEach(x => post.Comments.Add(x.ToComment()));

            return post;
        }
    }
}