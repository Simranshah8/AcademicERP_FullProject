
on_load =()=>{   
    document.getElementById('detail').style.display="none";  
	document.getElementById('check').style.display="none";   
	
}
	window.onload=on_load();




    document.getElementById('detail-btn').onclick = function(){
	document.getElementById('table-listing').style.display="none";
	document.getElementById('detail').style.display="block";
}

    document.getElementById('back-btn').onclick = function(){
	document.getElementById('table-listing').style.display="block";
	document.getElementById('detail').style.display="none";
}

document.getElementById('check-btn').onclick = function(){
	document.getElementById('check').style.display="block";
	document.getElementById('detail').style.display="none";
}

document.getElementById('back-detail-btn').onclick = function(){
	
	document.getElementById('detail').style.display="block";
	document.getElementById('check').style.display="none";
}