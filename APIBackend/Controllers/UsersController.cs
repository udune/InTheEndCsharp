using APIBackend.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace APIBackend.Controllers
{
    [Route("[controller]")] // /users
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<UserDto> Users = [
            new UserDto
            {
                Id = 1,
                Name = "홍길동",
                Username = "gildong",
                Email = "gildong@example.com",
                Address = new AddressDto
                {
                    Street = "테헤란로 123",
                    Suite = "101호",
                    City = "서울",
                    Zipcode = "06234",
                    Geo = new GeoDto { Lat = "37.5009", Lng = "127.0364" }
                },
                Phone = "010-1234-5678",
                Website = "gildong.example.com",
                Company = new CompanyDto
                {
                    Name = "길동소프트",
                    CatchPhrase = "빠르고 정확한 서비스",
                    Bs = "웹 서비스 개발"
                }
            },
            new UserDto
            {
                Id = 2,
                Name = "김철수",
                Username = "chulsoo",
                Email = "chulsoo@example.com",
                Address = new AddressDto
                {
                    Street = "해운대로 456",
                    Suite = "202호",
                    City = "부산",
                    Zipcode = "48094",
                    Geo = new GeoDto { Lat = "35.1631", Lng = "129.1635" }
                },
                Phone = "010-2345-6789",
                Website = "chulsoo.example.com",
                Company = new CompanyDto
                {
                    Name = "철수테크",
                    CatchPhrase = "기술로 여는 내일",
                    Bs = "모바일 앱 개발"
                }
            },
            new UserDto
            {
                Id = 3,
                Name = "이영희",
                Username = "younghee",
                Email = "younghee@example.com",
                Address = new AddressDto
                {
                    Street = "동성로 789",
                    Suite = "303호",
                    City = "대구",
                    Zipcode = "41939",
                    Geo = new GeoDto { Lat = "35.8714", Lng = "128.6014" }
                },
                Phone = "010-3456-7890",
                Website = "younghee.example.com",
                Company = new CompanyDto
                {
                    Name = "영희디자인",
                    CatchPhrase = "사용자를 위한 디자인",
                    Bs = "UI/UX 컨설팅"
                }
            }
        ];
        
        [HttpGet] // /users
        public IEnumerable<UserDto> GetUsers()
        {
            return Users;
        }

        [HttpGet("{userId:int}")]
        public ActionResult<UserDto> GetUser(int userId)
        {
            UserDto? user = Users.FirstOrDefault(user => user.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public ActionResult<UserDto> CreateUser([FromBody] UserDto user)
        {
            var headers = Request.Headers;
            
            user.Id = Users.Max(x => x.Id) + 1;
            Users.Add(user);
            
            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        }
    }
}
