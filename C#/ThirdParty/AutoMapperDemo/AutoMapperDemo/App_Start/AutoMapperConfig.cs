using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapperDemo.Models;

// http://cpratt.co/using-automapper-getting-started/
namespace AutoMapperDemo.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            /*
             *  Mappings
             */
            // AutoMapper.Mapper.CreateMap<SourceClass, DestinationClass>();
            AutoMapper.Mapper.CreateMap<BookModel, BookViewModel>()
                .ForMember(dest => dest.Author,
                    opts => opts.MapFrom(src => src.Author.Name));

            // map
            var author = new AuthorModel() {Name = "Jeremy Cantu"};
            var book = new BookModel() {Author = author, Title = "AutoMapper Demo"};

            var destinationObject = AutoMapper.Mapper.Map<BookViewModel>(book);

            // map back to BookModel from BookViewModel
            AutoMapper.Mapper.CreateMap<BookViewModel, BookModel>().ReverseMap();

            /*
             *  Projections
             */

            // AuthorFullNameModel contains first and last name, 
            // need to map many to one
            AutoMapper.Mapper.CreateMap<BookModel, BookViewModel>()
                .ForMember(dest => dest.AuthorFullName,
                            opts => opts.MapFrom(
                                src => string.Format("{0} {1}",
                                    src.AuthorFullName.FirstName,
                                    src.AuthorFullName.LastName)));

            // map
            var authorFullName = new AuthorFullNameModel() {FirstName = "R.L.", LastName = "Stine"};
            var bookTwo = new BookModel() { Author = author, AuthorFullName = authorFullName, Title = "Some Spooky Stuff" };

            var destinationObjectTwo = AutoMapper.Mapper.Map<BookViewModel>(bookTwo);

            /*
             *  Complex Mappings
             */

            AutoMapper.Mapper.CreateMap<PersonDtoModel, PersonModel>()
                .ForMember(dest => dest.Address,
                    opts => opts.MapFrom(
                        src => new AddressModel
                        {
                            Street = src.Street,
                            City = src.City,
                            State = src.State,
                            ZipCode = src.ZipCode
                        }));

            // map
            var personDto = new PersonDtoModel()
            {
                City = "Laredo", FirstName = "Jeremy", LastName = "Cantu", 
                State = "Texas", Street = "Nope", ZipCode = "99999"
            };

            var destinationObjectThree = AutoMapper.Mapper.Map<PersonModel>(personDto);
        }
    }
}