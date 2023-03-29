using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework.Models
{
    public class TaskModel
    {
        public  int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName ("Date and Time")]
        public DateTime DateTime { get; set; }

        [DisplayName("Task is Done")]
        public bool IsDone { get; set; }

        public TaskModel()
        {
            Id = -1;
            Title = "Nothing";
            Category = "";
            Description = "Nothing here";
            DateTime = DateTime.Now;
            IsDone = false;
        }

        public TaskModel(int id, string title, string category, string description, DateTime dateTime, bool isDone)
        {
            Id = id;
            Title = title;
            Category = category;
            Description = description;
            DateTime = dateTime;
            IsDone = isDone;
        }
    }
}