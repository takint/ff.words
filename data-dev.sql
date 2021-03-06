USE [FFWordsDB]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170921171051_InitDb', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171126161018_UpdateColumnName', N'2.0.0-rtm-26452')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20180322084223_UpdateEntries', N'2.0.1-rtm-125')
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [CreatedDate], [CreatedUser], [Description], [Inactivated], [Name], [UpdatedDate], [UpdatedUser]) VALUES (1, CAST(N'2017-12-01T00:00:00.0000000' AS DateTime2), N'TakiNT', N'About dev', 0, N'Programming', NULL, NULL)
INSERT [dbo].[Categories] ([Id], [CreatedDate], [CreatedUser], [Description], [Inactivated], [Name], [UpdatedDate], [UpdatedUser]) VALUES (2, CAST(N'2017-12-01T00:00:00.0000000' AS DateTime2), N'TakiNT', N'About dev relaxing', 0, N'Gamming', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Entries] ON 

INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (1, CAST(N'2017-12-01T00:00:00.0000000' AS DateTime2), N'TakiNT', N'<p><strong>First update data</strong></p><p><br></p><p><strong>how about the others</strong></p>', 0, CAST(N'2018-04-18T13:29:03.5726384' AS DateTime2), N'TakiNT', N'Test route 3', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (2, CAST(N'2017-12-01T00:00:00.0000000' AS DateTime2), N'TakiNT', N'Content 2', 0, NULL, NULL, N'Title 2', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (3, CAST(N'2017-12-01T00:00:00.0000000' AS DateTime2), N'TakiNT', N'Content 3', 0, NULL, NULL, N'Title 3', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (4, CAST(N'2017-12-01T00:00:00.0000000' AS DateTime2), N'TakiNT', N'Test content', 0, NULL, NULL, N'Test post', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (5, CAST(N'2017-12-01T15:38:13.5782093' AS DateTime2), N'TakiNT', N'Test Content', 0, NULL, NULL, N'Test 2', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (6, CAST(N'2017-12-01T15:38:25.9342610' AS DateTime2), N'TakiNT', N'123', 0, NULL, NULL, N'Test abc', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (7, CAST(N'2017-12-01T15:43:14.3105563' AS DateTime2), N'TakiNT', N'Check content', 0, NULL, NULL, N'ChickKent', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (8, CAST(N'2017-12-05T11:03:22.3997056' AS DateTime2), N'TakiNT', N'content 2', 0, NULL, NULL, N'test 123', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (9, CAST(N'2017-12-05T11:04:10.0597121' AS DateTime2), N'TakiNT', N'abc 123 345', 0, CAST(N'2017-12-05T11:06:08.1034416' AS DateTime2), N'TakiNT', N'test', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (10, CAST(N'2017-12-05T11:20:00.6156482' AS DateTime2), N'TakiNT', N'<script src="https://gist.github.com/takint/24bdce62754b50a3a857c6a58f5c9306.js"></script>', 0, CAST(N'2018-04-26T12:17:04.7762657' AS DateTime2), N'TakiNT', N'Test add', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (11, CAST(N'2018-04-18T13:40:00.5115161' AS DateTime2), N'TakiNT', N'<p class="ql-align-justify">On Tuesday, November 8, 2016 I’ll be&nbsp;<a href="https://qconsf.com/sf2016/speakers/phil-haack" target="_blank" style="color: rgb(68, 102, 197);">giving a talk entitled “Social Coding for Effective Teams and Products”</a>&nbsp;at QCon SF as part of the “Soft Skills” track. If you happen to be in San Francisco at that time, come check it out.</p><p class="ql-align-justify">In anticipation of this talk, I&nbsp;<a href="https://www.infoq.com/articles/engineering-culture-phil-haack" target="_blank" style="color: rgb(68, 102, 197);">recorded a podcast for InfoQ</a>&nbsp;where I pointed out the irony of using the term “soft skills” to describe the track as these are often the most challenging skills we deal with day to day. They are indeed the hard skills of being a software developer.</p><p class="ql-align-justify">In the podcast, we also cover what it was like in the early days of ASP.NET MVC as we went from closed source to open source and how far Microsoft has come since then in the open source space.</p><p class="ql-align-justify">Afterwards, we talked a bit about Atom and Electron and the community around those products. And to finish the podcast, we gabbed about my transition into management at GitHub, which&nbsp;<a href="https://haacked.com/archive/2016/09/06/work-at-github/" target="_blank" style="color: rgb(68, 102, 197);">is something I wrote about recently</a>.</p><p class="ql-align-justify">So if you don’t mind hearing my nasally voice, take a listen and let me know what you thought here.</p>', 0, CAST(N'2018-04-24T15:56:13.9689643' AS DateTime2), N'TakiNT', N'The Hard Skills', NULL, 0, NULL, NULL)
INSERT [dbo].[Entries] ([Id], [CreatedDate], [CreatedUser], [Content], [Inactivated], [UpdatedDate], [UpdatedUser], [Title], [AuthorName], [CurrentStatus], [Excerpt], [FeaturedImage]) VALUES (12, CAST(N'2018-05-04T00:00:00.0000000' AS DateTime2), N'TakiNT', N'<img width="150" src="http://res.cloudinary.com/ff-team/image/upload/v1525241453/avatar.png" /><br />
            <hr />
            <p>
                Jim is not my real name. I''m a Vietnamese so that I have a name in my native language.
                My family and friends used to call me Tin (it''s not a Western name and quite difficult to say).
                I choose Jim as my English name and for my international friends could easy to call me.
            </p>
            <p>
                Describe myself in short version: "A guy love games and have passion for programming".
            </p>
            <h2>What do I do?</h2>
            <hr />
            <p>
                People could call me a developer, a programmer or a coder.<br />
                Btw, I write code to make software, awesome software. That''s all! <br />
            </p>
            <h2>Why I choose to be a developer?</h2>
            <hr />
            <p>

            </p>
            <h2>What I do for fun?</h2>
            <hr />
            <p>
                Sometime I play some videos games.<br />
                I likes to share my knowledge with other IT lovers and less tech-savvy people.
            </p>
            <h2>Why I create this blog?</h2>
            <p>
                The main purpose is to keep track of whatever I am thinking about or working on.<br />
                I firmly believe that blogs are a two way conversation, so I welcome email and comments — as long as they’re on topic, more or less.<br />
                Fell free to drop me an email to <i>taki.lnt [at] gmail [dot] com.</i>
            </p>', 0, CAST(N'2018-05-04T00:00:00.0000000' AS DateTime2), N'TakiNT', N'About Jim', N'Jim', 4, NULL, N'http://res.cloudinary.com/ff-team/image/upload/v1525241453/avatar.png')
SET IDENTITY_INSERT [dbo].[Entries] OFF
