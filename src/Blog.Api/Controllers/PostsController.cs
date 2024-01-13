using Blog.Application.Posts.Commands.CreatePost;
using Blog.Application.Posts.Commands.DeletePostById;
using Blog.Application.Posts.Dtos;
using Blog.Application.Posts.Queries.GetLastPublishedPosts;
using Blog.Application.Posts.Queries.GetPostById;
using Blog.Application.Posts.Queries.GetPostsTree;
using Blog.Application.Posts.Queries.GetTotalPages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PostsController : ControllerBase
{
  private readonly IMediator _mediator;

  public PostsController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet("tree")]
  public async Task<IActionResult> GetPostsTree(CancellationToken cancellationToken = default)
  {
    var getTree = new GetPostsTreeQuery();

    var result = await _mediator.Send(getTree, cancellationToken);

    return result.IsSucceeded ? Ok(result.Data) : BadRequest(result.Error);
  }

  [HttpGet]
  public async Task<IActionResult> GetLastPublishedPosts([FromQuery] int page = 1, CancellationToken cancellationToken = default)
  {
    if (page < 1)
    {
      return BadRequest("Page cannot be less than 1.");
    }

    var getPosts = new GetLastPublishedPostsQuery(page);

    var result = await _mediator.Send(getPosts, cancellationToken);

    return result.IsSucceeded ? Ok(result.Data) : BadRequest(result.Error);
  }

  [HttpGet("pages")]
  public async Task<IActionResult> GetTotalPages(CancellationToken cancellationToken = default)
  {
    var getTotalPages = new GetTotalPagesQuery();

    var result = await _mediator.Send(getTotalPages, cancellationToken);

    return result.IsSucceeded ? Ok(result.Data) : BadRequest(result.Error);
  }

  [HttpGet("{postId}")]
  public async Task<IActionResult> GetPostById([FromRoute] string postId, CancellationToken cancellationToken = default)
  {
    var getPostById = new GetPostByIdQuery(postId);

    var result = await _mediator.Send(getPostById, cancellationToken);

    return result.IsSucceeded ? Ok(result.Data) : BadRequest(result.Error);
  }

  [HttpPost]
  public async Task<IActionResult> CreatePost(BlogPostDto blogPost, CancellationToken cancellationToken = default)
  {
    var createPost = new CreatePostCommand(blogPost);

    var result = await _mediator.Send(createPost, cancellationToken);

    return result.IsSucceeded ? Ok(result.Data) : BadRequest(result.Error);
  }

  [HttpDelete]
  public async Task<IActionResult> DeletePosts([FromQuery] string postId, CancellationToken cancellationToken = default)
  {
    var deletePost = new DeletePostByIdCommand(postId);

    var result = await _mediator.Send(deletePost, cancellationToken);

    return result.IsSucceeded ? Ok() : BadRequest(result.Error);
  }
}