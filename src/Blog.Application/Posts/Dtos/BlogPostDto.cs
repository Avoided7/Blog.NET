using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Posts.Dtos;

public sealed record BlogPostDto(
  [Required, MaxLength(200)] string Title,
  [Required, MaxLength(500)] string ShortDescription, 
  [Required, MaxLength(20000)] string Content,
  [Required] string ImagePath,
  [Required, MaxLength(100)] string Tags,
  [Required] bool IsPublished,
  DateTime? Schedule = null);