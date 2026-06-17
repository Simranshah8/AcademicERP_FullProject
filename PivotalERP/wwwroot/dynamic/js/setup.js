
on_load =()=>{   
    document.getElementById('source-form').style.display="none";
    document.getElementById('setup-complain-form').style.display="none";
	
}
	window.onload=on_load();




document.getElementById('add-source').onclick = function(){
	document.getElementById('source-section').style.display="none";
	document.getElementById('source-form').style.display="block";
}
document.getElementById('sourceback-btn').onclick = function(){
	document.getElementById('source-form').style.display="none";
	document.getElementById('source-section').style.display="block";
}


document.getElementById('add-setup-complain').onclick = function(){
	document.getElementById('setup-complain-section').style.display="none";
	document.getElementById('setup-complain-form').style.display="block";
}
document.getElementById('setup-complainback-btn').onclick = function(){
	document.getElementById('setup-complain-form').style.display="none";
	document.getElementById('setup-complain-section').style.display="block";
}

