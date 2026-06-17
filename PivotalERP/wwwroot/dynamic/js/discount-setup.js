on_load =()=>{
    document.getElementById("discount-type-form").style.display = "none";

  }
  
  window.onload=on_load();
  
  document.getElementById("add-discount-type").onclick = function () {
    document.getElementById("discount-type-section").style.display = "none";
    document.getElementById("discount-type-form").style.display = "block";
  };
  document.getElementById("back-discount-type").onclick = function () {
    document.getElementById("discount-type-section").style.display = "block";
    document.getElementById("discount-type-form").style.display = "none";
  };
  
  // Check box scripts
  $(document).ready(function () {
    $("#checkbox-include").click(function () {
      $(
        "#checkbox-Baishakh,#checkbox-Jestha,#checkbox-Ashadh,#checkbox-Shrawan,#checkbox-Bhadra,#checkbox-Ashwin,#checkbox-Kartik,#checkbox-Mangsir,#checkbox-Poush,#checkbox-Magh,#checkbox-Falgun,#checkbox-Chaitra"
      )
        .not(this)
        .prop("checked", this.checked);
    });
    $("#checkbox-include-fee").click(function () {
      $(
        "#checkbox-Baishakh-fee,#checkbox-Jestha-fee,#checkbox-Ashadh-fee,#checkbox-Shrawan-fee,#checkbox-Bhadra-fee,#checkbox-Ashwin-fee,#checkbox-Kartik-fee,#checkbox-Mangsir-fee,#checkbox-Poush-fee,#checkbox-Magh-fee,#checkbox-Falgun-fee,#checkbox-Chaitra-fee"
      )
        .not(this)
        .prop("checked", this.checked);
    });
  });
  