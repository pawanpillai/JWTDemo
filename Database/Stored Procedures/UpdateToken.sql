DELIMITER $$
CREATE PROCEDURE `UpdateToken`(
	IN _Id VARCHAR(100),
	IN _EmailAddress VARCHAR(100),
    IN _Token VARCHAR(1000),
    IN _IsStop VARCHAR(100),
	IN _ClientId VARCHAR(100),
    OUT _Response INT
)
BEGIN
    
	SET _Response = 0;		-- default value
    
	SELECT COUNT(Id) INTO @TokenExists FROM Tokens WHERE EmailAddress = _EmailAddress;
    
	IF (@TokenExists = 0) THEN		-- means token does not exists, create new token
		INSERT INTO Tokens 
		(
			Id,
			EmailAddress,
			Token,
			IsStop,
			ClientId
	       ) 
	       VALUES 
		(
			_Id,
			_EmailAddress,
			_Token,
			_IsStop,
			_ClientId
		);
	
	   	SET  _Response = 1;
	
	ELSE	-- means token exists, just update it
	
		UPDATE Tokens 
	        	SET Id = _Id,
				EmailAddress = _EmailAddress,
				Token = _Token,
				IsStop = _IsStop,
				ClientId = _ClientId
			WHERE EmailAddress = _EmailAddress;
           
		SET  _Response = 1;
	
	END IF;
	

END$$
DELIMITER ;
