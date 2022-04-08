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
  creatorId varchar(255) NOT NULL,
  recipeId int,
  name TEXT NOT NULL,
  quantity TEXT NOT NULL,
  FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8;
CREATE TABLE IF NOT EXISTS steps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId varchar(255) NOT NULL,
  recipeId int,
  ordr int,
  body TEXT NOT NULL,
  FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8;
CREATE TABLE IF NOT EXISTS favorites(
  id INT AUTO_INCREMENT primary key,
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  FOREIGN KEY(accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE
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
-- ALTER TABLE
  --   recipes
  -- ADD
  --   COLUMN picture TEXT NOT NULL
  -- INSERT INTO
  --   favorites (recipeId, accountId)
  -- VALUES
  --   (5, "624f5a354ed28ee7d96ea2b");
SELECT
  a.name,
  r.title,
  fv.recipeId
FROM
  favorites fv
  JOIN accounts a ON fv.accountId = a.id
  JOIN recipes r ON fv.recipeId = r.id
WHERE
  fv.recipeId = 5;
-- SELECT
  --   a.name,
  --   r.title,
  --   fv.recipeId
  -- FROM
  --   favorites fv
  --   JOIN accounts a ON fv.accountId = a.id
  --   JOIN recipes r ON fv.recipeId = r.id
  -- WHERE
  --   fv.accountId = "624f5a354ed28ee7d96ea2b4"