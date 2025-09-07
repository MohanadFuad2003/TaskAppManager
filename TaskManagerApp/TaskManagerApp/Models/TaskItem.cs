using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public bool IsDone { get; set; }

        // مستوى الصعوبة: Easy, Medium, Hard
        [Required(ErrorMessage = "Please select a difficulty.")]
        public string Difficulty { get; set; }

        // يُحدد تلقائياً حسب الصعوبة
        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }


        public string? UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
