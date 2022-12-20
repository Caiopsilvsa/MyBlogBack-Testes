using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Dto;
using MyBlog.Entities;
using MyBlog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyBlog.Test.Repositories
{
    public class PostRepositoryTests
    {
        private readonly IMapper _mapper;

        public PostRepositoryTests()
        {
            _mapper = A.Fake<IMapper>();
        }

        private async Task<DataContext> GetDataBaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if(await databaseContext.Posts.CountAsync() <= 0)
            {
                for (int i =1; i<=10; i++)
                {
                    databaseContext.Posts.AddRange(

                    new Post()
                    {
                        Author = "João Neto",
                        Titulo = "Dolor anim cupidatat",
                        SubTitulo = "Sunt esse aliqua ullamco in incididunt consequat commodo.",
                        Conteudo = "Dolor anim cupidatat occaecat aliquip et Lorem ut elit fugiat. Mollit eu pariatur est sunt. Minim fugiat sit do dolore eu elit ex do id sunt. Qui fugiat nostrud occaecat nisi est dolor qui fugiat laborum cillum. Occaecat consequat ex mollit commodo ad irure cillum nulla velit ex pariatur veniam cupidatat. Officia veniam officia non deserunt mollit.\r\n",
                        CreatedAt = DateTime.Now,
                        Categoria = "Esportes",
                    },
                    new Post()
                    {
                        Author = "Paulo Rodrigues",
                        Titulo = "Dolor anim cupidatat",
                        SubTitulo = "Sunt esse aliqua ullamco in incididunt consequat commodo.",
                        Conteudo = "Dolor anim cupidatat occaecat aliquip et Lorem ut elit fugiat. Mollit eu pariatur est sunt. Minim fugiat sit do dolore eu elit ex do id sunt. Qui fugiat nostrud occaecat nisi est dolor qui fugiat laborum cillum. Occaecat consequat ex mollit commodo ad irure cillum nulla velit ex pariatur veniam cupidatat. Officia veniam officia non deserunt mollit.\r\n",
                        CreatedAt = DateTime.Now,
                        Categoria = "Mundo"
                    }
                 ); ;
                    await databaseContext.SaveChangesAsync();
                }
                
            }
            return databaseContext;
        }

        [Fact]
        public async void PostRepository_GetPostById_ReturnPost()
        {
            //Arrange
            var id = 1;
            var dbContext = await GetDataBaseContext();
            var postRepository = new PostRepository(dbContext, _mapper);

            //Act
            var result = await postRepository.GetPostById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Post>();
        }

        
        [Fact]
        public async void PostRepository_GetPostByName_ReturnPostDto()
        {
            //Arrange
            var titleName = "Dolor anim cupidatat";
            var mappedResult = A.Fake<PostDto>();
            var dbContext = await GetDataBaseContext();
            var postRepository = new PostRepository(dbContext, _mapper);

            //Act
            var result =  postRepository.GetPostByName(titleName);

            //Assert
            result.Should().NotBeNull();     
        }

        [Fact]
        public async void PostRepository_GetPostByTitleName_ReturnsPost()
        {
            //Arrange
            var titleName = "Dolor anim cupidatat";
            var dbContext = await GetDataBaseContext();
            var postRepository = new PostRepository(dbContext, _mapper);

            //Act
            var result = postRepository.GetPostByTitleName(titleName);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<Post>));

        }

        [Fact]
        public async void PostRepository_GetPostEntity_ReturnsPost()
        {
            //Arrange
            var authorName = "João Neto";
            var dbContext = await GetDataBaseContext();
            var postRepository = new PostRepository(dbContext, _mapper);

            //Act
            var result = postRepository.GetPostEntity(authorName);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<Post>));

        }

        [Fact]

        public async void PostRepository_GetPosts_ReturnsPostDto()
        {
            //Arrange
            var dbContext = await GetDataBaseContext();
            var postRepository = new PostRepository(dbContext, _mapper);

            //Act
            var result = postRepository.GetPosts();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IEnumerable<PostDto>>));
        }

        [Fact]
        public async void PostRepository_SaveChanges_ReturnsTrue()
        {
            //Arrange
            var Post = new Post()
            {
                Author = "João Paulo",
                Titulo = "Dolor anim cupidatat",
                SubTitulo = "Sunt esse aliqua ullamco in incididunt consequat commodo.",
                Conteudo = "Dolor anim cupidatat occaecat aliquip et Lorem ut elit fugiat. Mollit eu pariatur est sunt. Minim fugiat sit do dolore eu elit ex do id sunt. Qui fugiat nostrud occaecat nisi est dolor qui fugiat laborum cillum. Occaecat consequat ex mollit commodo ad irure cillum nulla velit ex pariatur veniam cupidatat. Officia veniam officia non deserunt mollit.\r\n",
                CreatedAt = DateTime.Now,
                Categoria = "Esportes",
            };
            var dbContext = await GetDataBaseContext();
            var postRepository = new PostRepository(dbContext, _mapper);

            //Act
            postRepository.NewPost(Post);
            var result = postRepository.SaveChanges();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<bool>));
        }

        [Fact]
        public async void PostRepository_DeletePost_ReturnsTrue()
        {
            //Arrange
            var Post = new Post()
            {
                Author = "João Paulo",
                Titulo = "Dolor anim cupidatat",
                SubTitulo = "Sunt esse aliqua ullamco in incididunt consequat commodo.",
                Conteudo = "Dolor anim cupidatat occaecat aliquip et Lorem ut elit fugiat. Mollit eu pariatur est sunt. Minim fugiat sit do dolore eu elit ex do id sunt. Qui fugiat nostrud occaecat nisi est dolor qui fugiat laborum cillum. Occaecat consequat ex mollit commodo ad irure cillum nulla velit ex pariatur veniam cupidatat. Officia veniam officia non deserunt mollit.\r\n",
                CreatedAt = DateTime.Now,
                Categoria = "Esportes",
            };
            var dbContext = await GetDataBaseContext();
            var postRepository = new PostRepository(dbContext, _mapper);

            //Act
            postRepository.DeletePost(Post);
            var result = postRepository.SaveChanges();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<bool>));
        }
    }
}

