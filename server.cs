registerOutputEvent(Minigame,playSound,"dataBlock Sound",0);
function MinigameSO::playSound(%this, %sound)
{
	if(!isObject(%sound) || %sound.getClassName() !$= "AudioProfile")
		return;
	 
	if(%sound.description.isLooping)
		return;
	 
	for(%i = 0; %i < %this.numMembers; %i++)
	{
		%cl = %this.member[%i];
		//check for hacky dedicated minigame mods or AIConnections
		if(isObject(%cl) && %cl.getClassName() $= "GameConnection")
			%cl.play2D(%sound);
	}
}

function MinigameSO::play2D(%this, %sound)
{
	%this.playSound(%sound);
}

//Score

function GameConnection::getScore(%this)
{
	return %this.score;
}

//Hex

function hexToRgb(%rgb)
{
	 %r = _hexToComp(getSubStr(%rgb,0,2)) / 255;
	 %g = _hexToComp(getSubStr(%rgb,2,2)) / 255;
	 %b = _hexToComp(getSubStr(%rgb,4,2)) / 255;
	 return %r SPC %g SPC %b;
}
 
function _compToHex(%comp)
{
	 %left = mFloor(%comp / 16);
	 %comp = mFloor(%comp - %left * 16);
	 %left = getSubStr("0123456789ABCDEF",%left,1);
	 %comp = getSubStr("0123456789ABCDEF",%comp,1);
	 return %left @ %comp;
}
 
function _hexToComp(%hex)
{
	 %left = getSubStr(%hex,0,1);
	 %comp = getSubStr(%hex,1,1);
	 %left = striPos("0123456789ABCDEF",%left);
	 %comp = striPos("0123456789ABCDEF",%comp);
	 if(%left < 0 || %comp < 0)
		  return 0;
	 return %left * 16 + %comp;
}

function rgbToHex(%rgb)
{
	 %r = _compToHex(255 * getWord(%rgb,0));
	 %g = _compToHex(255 * getWord(%rgb,1));
	 %b = _compToHex(255 * getWord(%rgb,2));
	 return %r @ %g @ %b;
}

function redToGreen(%a)
{
	%r = 1;
	%g = 1;
	if(%a >= (1/2))
		%g = mAbs(%a - 1) * 2;
	if(%a < (1/2))
		%r = %a * 2;
	return %r SPC %g SPC "0";
}

function greenToRed(%a)
{
	%r = 1;
	%g = 1;
	if(%a >= (1/2))
		%r = mAbs(%a - 1) * 2;
	if(%a < (1/2))
		%g = %a * 2;
	return %r SPC %g SPC "0";
}

function hasItemOnList(%list, %item)
{
	if(getWordCount(%list) <= 0)
		return false;

	for(%i = 0; %i < getWordCount(%list); %i++)
	{
		%word = getWord(%list, %i);
		if(%word $= %item)
			return true;
	}

	return false;
}

function addItemToList(%list, %item)
{
	if(hasItemOnList(%list, %item))
		return %list;

	%list = trim(%list SPC %item);
	return %list;
}

function removeItemFromList(%list, %item)
{
	for(%i = 0; %i < getWordCount(%list); %i++)
	{
		%word = getWord(%list, %i);
		if(%word $= %item)
		{
			%list = removeWord(%list, %i);
			return %list;
		}
	}

	return %list;
}

function hasFieldOnList(%list, %item)
{
	if(getFieldCount(%list) <= 0)
		return false;

	for(%i = 0; %i < getFieldCount(%list); %i++)
	{
		%word = getField(%list, %i);
		if(%word $= %item)
			return true;
	}

	return false;
}

function addFieldToList(%list, %item)
{
	if(hasFieldOnList(%list, %item))
		return %list;

	%list = trim(%list TAB %item);
	return %list;
}

function removeFieldFromList(%list, %item)
{
	for(%i = 0; %i < getFieldCount(%list); %i++)
	{
		%word = getField(%list, %i);
		if(%word $= %item)
		{
			%list = removeWord(%list, %i);
			return %list;
		}
	}

	return %list;
}