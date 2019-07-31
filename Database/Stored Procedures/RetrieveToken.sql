DELIMITER $$
CREATE PROCEDURE `RetrieveToken`(
	IN _EmailAddress VARCHAR(100),
	IN _Token VARCHAR(100),
	OUT _Response INT
)
BEGIN
	
	SET _Response = 0;		-- default value
	
	SELECT COUNT(Id) INTO @TokenExists FROM Tokens WHERE EmailAddress = _EmailAddress AND Token = _Token AND IsStop = 0;
	
	IF (@TokenExists > 0) THEN		-- means token matched, return true
		
	   	SET  _Response = 1;

	END IF;
	
END$$
DELIMITER ;
