
on_load =()=>{
	document.getElementById('nominal-fee-form').style.display="none";
	
}
	window.onload=on_load();


document.getElementById('add-nominal-fee').onclick = function(){
	document.getElementById('nominal-fee-section').style.display="none";
	document.getElementById('nominal-fee-form').style.display="block";
}
document.getElementById('nominal-fee-back-btn').onclick = function(){
	document.getElementById('nominal-fee-form').style.display="none";
	document.getElementById('nominal-fee-section').style.display="block";
}
document.getElementById('edit-nominal-fee').onclick = function(){
	document.getElementById('nominal-fee-section').style.display="none";
	document.getElementById('nominal-fee-form').style.display="block";
}
