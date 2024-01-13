namespace Blog.Application.Posts.Dtos;

public sealed record PostsTreeDto(IEnumerable<PostTreeNodeDto> Nodes);

public sealed record PostTreeNodeDto(string Id, string Title, DateTime PublishedAt);