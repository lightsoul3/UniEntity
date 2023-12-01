using System.Text;
using NoSql.Services;
using NoSql.Models;
using DbConnection = NoSql.Database.DbConnection;
using MongoDB.Driver;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.Configure<DbConnection>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<ServiceConnection>();
builder.Services.AddSingleton(typeof(GetCollection<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Headers.ContentType = new Microsoft.Extensions.Primitives.StringValues("text/html; charset=UTF-8");
    var mainString = new StringBuilder();
    mainString.Append("<div><a href=\"/\" style=\"float: left; margin-right: 30px; \"><img class=\"logo\" src=\"https://cdn-icons-png.flaticon.com/512/2995/2995470.png\" alt=\"Logo\" style=\"width: 100px; height: auto;\"></a></div>");
    mainString.Append("<div style=\"text-align:center;\">");
    mainString.Append("<h1>School Ratings</h1>");
    mainString.Append("<h3 style=\"text-decoration: none;\"><a href=\"/ratings\">Ratings</a></h3>");
    mainString.Append("</div>");
    mainString.Append("</body></html>");

    await context.Response.WriteAsync(mainString.ToString());
});

app.MapGet("/ratings", async (HttpContext context) =>
{
    context.Response.Headers.ContentType = new Microsoft.Extensions.Primitives.StringValues("text/html; charset=UTF-8");
    var ratingString = new StringBuilder();

    var ratingService = app.Services.GetRequiredService<GetCollection<Rating>>();
    var ratings = await ratingService.GetAll();

    // Фільтруємо записи, у яких є більше одного запису в полі "Subject"
    var filteredRatings = ratings.Where(rating => rating.Student.Any(s => s.Subject.Count >= 2)).ToList();

    if (filteredRatings.Count > 0)
    {
        ratingString.Append("<style>");
        ratingString.Append("table { border-collapse: collapse; width: 100%; }");
        ratingString.Append("th, td { text-align: left; padding: 6px; border: 1px solid black; }");
        ratingString.Append("tr:nth-child(even)");
        ratingString.Append("</style>");
        ratingString.Append("<a href=\"/\" style=\"float: left; margin-right: 30px;\">");
        ratingString.Append("<img class=\"logo\" src=\"https://cdn-icons-png.flaticon.com/512/2995/2995470.png\" alt=\"Logo\" style=\"width: 100px; height: auto;\">");
        ratingString.Append("</a>");

        ratingString.Append("<h1><a href=\"/\">Home</a></h1><br/><br/><br/><br/><h1>List of ratings with more than one Subject</h1>");

        foreach (var rating in filteredRatings)
        {
            ratingString.Append($"<h2>ID: {rating.ID}</h2>");
            ratingString.Append($"<h3>Month: {rating.Month}</h3>");

            ratingString.Append("<table>");
            ratingString.Append("<thead><tr><th>Fullname</th><th>Phone</th><th>DateOfBirth</th><th>Address</th></tr></thead><tbody>");

            foreach (var student in rating.Student)
            {
                ratingString.Append($"<tr><td>{student.Fullname}</td><td>{student.Phone}</td><td>{student.DateOfBirth}</td><td>{student.Address}</td></tr>");

                ratingString.Append("<thead><tr><th>Subject</th><th>MaxRating</th><th>CurrentRating</th><th>Teacher</th></tr></thead><tbody>");
                foreach (var subject in student.Subject)
                {
                    ratingString.Append($"<tr><td>{subject.Name}</td><td>{subject.MaxRating}</td><td>{subject.CurrentRating}</td><td>{subject.Teacher[0].Name}</td></tr>");
                }
                ratingString.Append("</tbody>");
            }

            ratingString.Append("</tbody></table>");
        }

        await context.Response.WriteAsync("<html><body>" + ratingString.ToString() + "</body></html>");
    }
    else
    {
        await context.Response.WriteAsync("No ratings with more than one Subject found.");
    }
});

app.UseStaticFiles();

app.Run();