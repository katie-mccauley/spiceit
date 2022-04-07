CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS recipes(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId varchar(255) NOT NULL,
  title TEXT NOT NULL,
  subtitle TEXT NOT NULL,
  category TEXT NOT NULL,
  FOREIGN KEY(creatorId) REFERENCES accounts(id)
) default charset utf8;
CREATE TABLE IF NOT EXISTS ingredients(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  recipeId int,
  name TEXT NOT NULL,
  quantity TEXT NOT NULL,
  FOREIGN KEY(recipeId) REFERENCES recipes(id)
) default charset utf8;
CREATE TABLE IF NOT EXISTS steps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  recipeId int,
  ordr int,
  body TEXT NOT NULL,
  FOREIGN KEY(recipeId) REFERENCES recipes(id)
) default charset utf8;
DROP TABLE IF EXISTS recipes;
DROP TABLE IF EXISTS ingredients;
DROP TABLE IF EXISTS steps;
INSERT INTO
  recipes(title, subtitle, category, creatorId)
VALUES
  (
    "VOODOOO CAKE",
    "this is a cake that has magic",
    "magic",
    "624f5a354ed28ee7d96ea2b4"
  );