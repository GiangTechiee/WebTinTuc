USE [WebNC]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/20/2025 2:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 3/20/2025 2:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 3/20/2025 2:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](1000) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[IsApprove] [bit] NOT NULL,
	[Fk_UserId] [int] NOT NULL,
	[Fk_NewId] [int] NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 3/20/2025 2:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[Fk_UserId] [int] NOT NULL,
	[Fk_NewId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Favorites] PRIMARY KEY CLUSTERED 
(
	[Fk_UserId] ASC,
	[Fk_NewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 3/20/2025 2:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[NewId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Abstract] [nvarchar](500) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[PostedDate] [datetime2](7) NOT NULL,
	[ViewCount] [int] NOT NULL,
	[IsApprove] [bit] NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[NewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsCategory]    Script Date: 3/20/2025 2:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsCategory](
	[CategoriesCategoryId] [int] NOT NULL,
	[NewsNewId] [int] NOT NULL,
 CONSTRAINT [PK_NewsCategory] PRIMARY KEY CLUSTERED 
(
	[CategoriesCategoryId] ASC,
	[NewsNewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/20/2025 2:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/20/2025 2:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[PhoneNumber] [nvarchar](12) NOT NULL,
	[Avatar] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Fk_RoleId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[IsEmailConfirmed] [bit] NOT NULL,
	[EmailConfirmationToken] [nvarchar](max) NULL,
	[TokenExpiration] [datetime2](7) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250318104412_CreateDB', N'9.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250319140302_RenameCreateAt', N'9.0.3')
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([CommentId], [Content], [CreatedAt], [IsApprove], [Fk_UserId], [Fk_NewId], [ParentId]) VALUES (3, N'bj', CAST(N'2025-03-20T14:00:38.0502188' AS DateTime2), 0, 8, 2, NULL)
INSERT [dbo].[Comments] ([CommentId], [Content], [CreatedAt], [IsApprove], [Fk_UserId], [Fk_NewId], [ParentId]) VALUES (4, N'cac', CAST(N'2025-03-20T14:01:12.0541418' AS DateTime2), 0, 8, 4, NULL)
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (1, N'Hội thảo Công nghệ AI 2025 tại Hà Nội', N'Hiệp hội CNTT tổ chức hội thảo về AI.', N'Hội thảo quy tụ các chuyên gia hàng đầu để thảo luận về ứng dụng AI trong doanh nghiệp...', CAST(N'2025-03-01T09:00:00.0000000' AS DateTime2), 250, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (2, N'Xu hướng phát triển phần mềm mã nguồn mở', N'Mã nguồn mở đang thay đổi ngành CNTT.', N'Tìm hiểu cách các doanh nghiệp Việt Nam áp dụng mã nguồn mở...', CAST(N'2025-03-02T10:15:00.0000000' AS DateTime2), 180, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (3, N'Hiệp hội ra mắt chương trình đào tạo kỹ sư AI', N'Chương trình mới cho kỹ sư trẻ.', N'Đào tạo chuyên sâu về AI với sự hỗ trợ từ các công ty công nghệ lớn...', CAST(N'2025-03-03T13:30:00.0000000' AS DateTime2), 300, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (4, N'Công nghệ 6G: Tương lai của kết nối', N'6G hứa hẹn tốc độ vượt trội.', N'Bài viết phân tích lộ trình phát triển 6G tại Việt Nam...', CAST(N'2025-03-04T08:45:00.0000000' AS DateTime2), 150, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (5, N'Hội viên hiệp hội đạt giải thưởng quốc tế', N'Thành tựu mới trong lĩnh vực CNTT.', N'Một hội viên đã giành giải thưởng lớn tại Tech Awards 2025...', CAST(N'2025-03-05T11:00:00.0000000' AS DateTime2), 200, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (6, N'Blockchain trong quản lý chuỗi cung ứng', N'Ứng dụng blockchain tại Việt Nam.', N'Hiệp hội hợp tác với doanh nghiệp triển khai blockchain...', CAST(N'2025-03-06T14:20:00.0000000' AS DateTime2), 120, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (7, N'Thảo luận về an ninh mạng tại hội nghị', N'An ninh mạng là ưu tiên hàng đầu.', N'Hội nghị bàn về các mối đe dọa mạng mới nhất...', CAST(N'2025-03-07T09:30:00.0000000' AS DateTime2), 170, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (8, N'Hiệp hội tổ chức Ngày Công nghệ 2025', N'Sự kiện lớn nhất trong năm của hiệp hội.', N'Giới thiệu công nghệ mới và kết nối doanh nghiệp...', CAST(N'2025-03-08T12:00:00.0000000' AS DateTime2), 350, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (9, N'Phát triển kỹ năng lập trình cho sinh viên', N'Chương trình hỗ trợ sinh viên CNTT.', N'Hiệp hội phối hợp với trường đại học tổ chức khóa học...', CAST(N'2025-03-09T10:45:00.0000000' AS DateTime2), 90, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (10, N'Ứng dụng IoT trong công nghiệp 4.0', N'IoT đang thay đổi sản xuất.', N'Bài viết chi tiết về cách IoT được áp dụng tại Việt Nam...', CAST(N'2025-03-10T13:15:00.0000000' AS DateTime2), 140, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (11, N'Hội thảo về chuyển đổi số doanh nghiệp', N'Chuyển đổi số là xu hướng tất yếu.', N'Hiệp hội hỗ trợ doanh nghiệp nhỏ áp dụng chuyển đổi số...', CAST(N'2025-03-11T08:00:00.0000000' AS DateTime2), 160, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (12, N'Công nghệ thực tế ảo trong đào tạo', N'VR nâng cao chất lượng giáo dục.', N'Ứng dụng VR trong các khóa học kỹ thuật của hiệp hội...', CAST(N'2025-03-12T11:30:00.0000000' AS DateTime2), 110, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (13, N'Hiệp hội công bố báo cáo công nghệ 2025', N'Báo cáo mới về xu hướng CNTT.', N'Tổng hợp dữ liệu từ các hội viên và chuyên gia...', CAST(N'2025-03-13T14:00:00.0000000' AS DateTime2), 220, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (14, N'Tự động hóa và tương lai việc làm', N'Tác động của tự động hóa đến lao động.', N'Phân tích từ góc nhìn của hiệp hội nghề nghiệp...', CAST(N'2025-03-14T09:15:00.0000000' AS DateTime2), 130, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (15, N'Hội viên giới thiệu giải pháp đám mây', N'Đám mây giúp tối ưu chi phí.', N'Một hội viên chia sẻ kinh nghiệm triển khai cloud...', CAST(N'2025-03-15T12:45:00.0000000' AS DateTime2), 100, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (16, N'Công nghệ xanh: Giảm thiểu carbon', N'CNTT góp phần bảo vệ môi trường.', N'Hiệp hội kêu gọi áp dụng công nghệ xanh trong doanh nghiệp...', CAST(N'2025-03-16T15:00:00.0000000' AS DateTime2), 85, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (17, N'Hội thảo quốc tế về dữ liệu lớn', N'Big Data thay đổi cách quản lý dữ liệu.', N'Sự kiện quy tụ chuyên gia từ Việt Nam và nước ngoài...', CAST(N'2025-03-17T10:00:00.0000000' AS DateTime2), 190, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (18, N'Phát triển ứng dụng di động: Xu hướng 2025', N'App di động ngày càng quan trọng.', N'Hiệp hội tổ chức workshop về phát triển app...', CAST(N'2025-03-18T13:30:00.0000000' AS DateTime2), 145, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (19, N'Hiệp hội hợp tác với công ty công nghệ lớn', N'Hợp tác chiến lược với đối tác quốc tế.', N'Tăng cường nghiên cứu và phát triển công nghệ mới...', CAST(N'2025-03-19T08:45:00.0000000' AS DateTime2), 210, 1, N'new1.jpg')
INSERT [dbo].[News] ([NewId], [Title], [Abstract], [Content], [PostedDate], [ViewCount], [IsApprove], [Image]) VALUES (20, N'Công nghệ lượng tử: Bước tiến mới', N'Lượng tử mở ra kỷ nguyên mới cho CNTT.', N'Giới thiệu cơ bản về công nghệ lượng tử từ hiệp hội...', CAST(N'2025-03-20T11:15:00.0000000' AS DateTime2), 175, 1, N'new1.jpg')
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [FullName], [DateOfBirth], [PhoneNumber], [Avatar], [Email], [PasswordHash], [Fk_RoleId], [CreatedAt], [Address], [IsEmailConfirmed], [EmailConfirmationToken], [TokenExpiration]) VALUES (8, N'Trần Giang', CAST(N'2004-06-21T00:00:00.0000000' AS DateTime2), N'0386677621', N'default-avatar.jpg', N'giangtt8726@gmail.com', N'$2a$11$/fnY7jPxR7x8X20ntQCy1OoGqDVTQXDnRkCTOlp1iCMQTTnTEvAxa', 1, CAST(N'2025-03-20T13:59:16.9224862' AS DateTime2), NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Comments_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Comments] ([CommentId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Comments_ParentId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_News_Fk_NewId] FOREIGN KEY([Fk_NewId])
REFERENCES [dbo].[News] ([NewId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_News_Fk_NewId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users_Fk_UserId] FOREIGN KEY([Fk_UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users_Fk_UserId]
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Favorites_News_Fk_NewId] FOREIGN KEY([Fk_NewId])
REFERENCES [dbo].[News] ([NewId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK_Favorites_News_Fk_NewId]
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Favorites_Users_Fk_UserId] FOREIGN KEY([Fk_UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK_Favorites_Users_Fk_UserId]
GO
ALTER TABLE [dbo].[NewsCategory]  WITH CHECK ADD  CONSTRAINT [FK_NewsCategory_Categories_CategoriesCategoryId] FOREIGN KEY([CategoriesCategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsCategory] CHECK CONSTRAINT [FK_NewsCategory_Categories_CategoriesCategoryId]
GO
ALTER TABLE [dbo].[NewsCategory]  WITH CHECK ADD  CONSTRAINT [FK_NewsCategory_News_NewsNewId] FOREIGN KEY([NewsNewId])
REFERENCES [dbo].[News] ([NewId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsCategory] CHECK CONSTRAINT [FK_NewsCategory_News_NewsNewId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_Fk_RoleId] FOREIGN KEY([Fk_RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles_Fk_RoleId]
GO
