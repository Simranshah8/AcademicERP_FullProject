on_load =()=>{
	document.getElementById('image').style.display="none";	
}

window.onload=on_load();


// Radio Button Click
document.getElementById('inlineRadio1').onclick = function(){
document.getElementById('text').style.display="block";
document.getElementById('image').style.display="none";	
}

document.getElementById('inlineRadio2').onclick = function(){
document.getElementById('text').style.display="none";
document.getElementById('image').style.display="block";	
}

