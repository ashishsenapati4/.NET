namespace ViewExample.Models
{
    public class CommentService
    {
        public static async Task<List<Comment>> GetRecentCommentsAsync()
        {
            await Task.Delay(500);
            return new List<Comment>
            { 
                new Comment{Text="This is a great post!", User="Alice"},
                new Comment{Text="Independence is my fundamental right", User="MK Gandhi"},
                new Comment{Text="Kashmir is ours. Woof Woof.", User="Asim Munir"}
            };
        }
    }
}
