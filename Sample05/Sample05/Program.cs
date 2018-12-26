using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sample05
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Select_ContentWithComment();
            Console.Read();
        }
        static void test_insert()
        {
            var content = new Content();
            content.title = "标题1";
            content.content = "内容1";
            using (var con = new SqlConnection("Data Source=.;Database=Czar.cms;Integrated Security=true")) {
                string sql_insert = @"INSERT INTO Content(title, [content], status, add_time, modify_time) values
                (@title, @content,@status, @add_time, @modify_time)";
                var result = con.Execute(sql_insert, content);
                Console.WriteLine($"test_insert:插入了{result}条数据!");
            }
        }
        static void test_muti_insert()
        {
            List<Content> contents = new List<Content>() {
                new Content{title="批量插入标题1", content="批量插入内容1" },
                new Content{ title="批量插入标题2", content="批量插入内容2"},
                new Content{ title="批量插入标题3", content="批量插入内容3" }
            };
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=ekko;Password=Xzc94;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;")) {
                string sql_insert = @"insert into content (title, content, status, add_time, modify_time) 
                        values(@title, @content, @status, @add_time, @modify_time)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_muti_insert 插入了{result}条数据");
            }
        }
        static void test_del()
        {
            var content = new Content {
                id=2
            };
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=ekko;Password=Xzc94;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_delete = @"delete from content where id=@id";
                var result = conn.Execute(sql_delete, content);
                Console.WriteLine($"test_del删除了{result}条数据");

            }
        }
        static void test_mult_del()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                id=3,

            },
               new Content
            {
                id=1,

            },
            };
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=ekko;Password=Xzc94;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_delete = @"delete from content where id=@id";
                var result = conn.Execute(sql_delete, contents);
                Console.WriteLine($"test_del删除了{result}条数据");

            }
        }
        static void test_update()
        {
            var content = new Content {
                id=5,
                title = "标题5",
                content = "内容5",
            };
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=ekko;Password=Xzc94;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_update = @"update content set title=@title,content=@content where id=@id";
                var result = conn.Execute(sql_update, content);
                Console.WriteLine($"test_update修改了{result}条数据");

            }
        }
        static void test_mult_update()
        {
            List<Content> contents = new List<Content>() {
                new Content{
                    id=8,
                    title="批量修改的标题8",
                    content="批量修改的内容8"
                },
                new Content{
                    id=9,
                    title="批量修改的标题9",
                    content="批量修改的内容9"
                }
            };
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=ekko;Password=Xzc94;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_mult_update = @"update content set title=@title,content=@content where id=@id";
                var result = conn.Execute(sql_mult_update, contents);
                Console.WriteLine($"test_mult_update修改了{result}条数据");

            }
        }
        static void test_select_one() {
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=ekko;Password=Xzc94;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_select = @"select * from content where id=@id";
                var result = conn.QueryFirstOrDefault<Content>(sql_select, new { id = 7 });
                Console.WriteLine($"test_select_one：查到的数据为：{result}");

            }
        }
        static void test_select_list() {
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=ekko;Password=Xzc94;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_select = @"select * from [content] where id in @ids";
                var result = conn.Query<Content>(sql_select, new { ids = new int[] { 7, 8 } });
                Console.WriteLine($"test_select_list：查到的数据为：{result}");

            }
        }
        static void test_comment_insert(int content_id)
        {
            var comment = new Comment {
                content_id=content_id,
                content="评论"
            };
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=ekko;Password=Xzc94;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"insert into comment (content_id, content) values (@content_id, @content)";
                var result = conn.Execute(sql_insert, comment);
                Console.WriteLine($"test_comment_insert插入：{result}行数据");

            }
        }
        static void Select_ContentWithComment() {
            using (var conn = new SqlConnection("Data Source=127.0.0.1;User ID=ekko;Password=Xzc94;Initial Catalog=Czar.Cms;Pooling=true;Max Pool Size=100;"))
            {
                var sql = @"select * from content where id=@id; select * from comment where content_id=@id";
                using (var result = conn.QueryMultiple(sql, new { id = 7 })) {
                    var content = result.ReadFirstOrDefault<ContentWithComment>();
                    content.comments = result.Read<Comment>();
                    Console.WriteLine($"test_select_content_with_comment:内容7的评论数量{content.comments.Count()}");
                }
            }
        }
    }
}
