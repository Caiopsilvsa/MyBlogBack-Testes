using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Controllers;
using MyBlog.Dto;
using MyBlog.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBlog.Test.Controllers
{
    public class PostControllerTests
    {
        private readonly IPostInterface _postRepository;
        private readonly IMapper _mapper;

        public PostControllerTests()
        {
            _postRepository = A.Fake<IPostInterface>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void PostController_GetPost_PostDto()
        {
            //Arrange
            var posts = A.Fake<List<PostDto>>();
            var controller = new PostController(_postRepository, _mapper);

            //Act
            var result = controller.GetPost();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ActionResult<IEnumerable<PostDto>>>));
        }
    }
}
