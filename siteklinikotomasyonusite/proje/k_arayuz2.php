<?php
	
	
	
	include 'php/baglanti.php';
	

	session_start();
	
	
	
	if (!isset($_SESSION['tc_no'])) {
		
	header("Location: /proje/login.html");
	exit();
	}
	
	
	
	
	$tc_no=$_SESSION['tc_no'];
	
	
	if(isset($_POST['kaydet'])){
	
	if (isset($_POST['adi'])) {
    $adi = $_POST['adi'];
	} else {
    $adi = '';
	}
	if (isset($_POST['soyadi'])) {
    $soyadi = $_POST['soyadi'];
	} else {
    $soyadi = '';
	}
	if (isset($_POST['cinsiyet'])) {
    $cinsiyet = $_POST['cinsiyet'];
	} else {
    $cinsiyet = '';
	}
	if (isset($_POST['dogum_tarihi'])) {
    $dogum_tarihi = $_POST['dogum_tarihi'];
	} else {
    $dogum_tarihi = '';
	}
	if (isset($_POST['medeni_hal'])) {
    $medeni_hal = $_POST['medeni_hal'];
	} else {
    $medeni_hal = '';
	}
	
	
	$sql = "UPDATE hastabilgileri SET ";
$params = array();

if (!empty($_POST['adi']) && $_POST['adi'] != $_SESSION['adi']) {
    $sql .= "adi = ?,";
    $params[] = $_POST['adi'];
}

if (!empty($_POST['soyadi']) && $_POST['soyadi'] != $_SESSION['soyadi']) {
    $sql .= "soyadi = ?,";
    $params[] = $_POST['soyadi'];
}

if (!empty($_POST['cinsiyet']) && $_POST['cinsiyet'] != $_SESSION['cinsiyet']) {
    $sql .= "cinsiyet = ?,";
    $params[] = $_POST['cinsiyet'];
}

if (!empty($_POST['dogum_tarihi']) && $_POST['dogum_tarihi'] != $_SESSION['dogum_tarihi']) {
    $sql .= "dogum_tarihi = ?,";
    $params[] = $_POST['dogum_tarihi'];
}

if (!empty($_POST['medeni_hal']) && $_POST['medeni_hal'] != $_SESSION['medeni_hal']) {
    $sql .= "medeni_hal = ?,";
    $params[] = $_POST['medeni_hal'];
}

// Remove the trailing comma from the query string
$sql = rtrim($sql, ",");

// Add the WHERE clause to update only the record for the logged in user
$sql .= " WHERE tc_no = ?";
$params[] = $_SESSION['tc_no'];

$stmt = sqlsrv_query($conn, $sql, $params);

if (!$stmt) {
    die(print_r(sqlsrv_errors(), true));
}

sqlsrv_close($conn);

header('Location: /proje/k_arayuz.php');
	
	}
?>



<!DOCTYPE html>
<html>
<head>


<link rel="stylesheet" href="css/k_arayuz2.css">
<meta charset="UTF-8">


</head>
<body>
<div class="ust_serit"></div>
<div class="header">

<div class="logo">

  <img src="img/logo.jpg" style="width:500px; height:100px; float:left;">

</div>

<a href="index.html" class="linkler">Anasayfa</a>

<a href="index2.html" class="linkler">Hakkımızda</a>

<a href="index3.html" class="linkler">Galeri</a>

<a href="index4.html" class="linkler">İletişim</a>

<a href="login.html" class="linkler" id="giris_b" style="background-color:#FF0B5C; float:right; color:white;">Giriş</a>

<a href="k_arayuz.php" class="linkler" id="k_arayuz" style="background-color:#C0EB56; color:white;  float:right; ">kullanıcı</a>


</div>


<div class="orta" style="background-image: url('img/k_arayuz.png'); background-size: cover;">



<div id="kutu">


	<form method="post" action="" style="margin-top:50px;">
	
	
	<h1>tc_no: &nbsp; <input type="text"  name="" value="<?php echo $tc_no; ?>" style="width:250px; height:25px; margin-left:105px;" disabled></h1>
	<br>
	<h1>adi: &nbsp; <input type="text"  name="adi" style="width:250px; height:25px; margin-left:135px;"> </h1> 
	<br>
	<h1>soyadi: &nbsp; <input type="text" name="soyadi" style="width:250px; height:25px; margin-left:92px;"> </h1>
	<br>
	<h1>cinsiyet: &nbsp; <input type="radio" id="erkek" name="cinsiyet" value="erkek" style="margin-left:100px;" >
						 <label for="erkek">erkek</label>
						 <input type="radio" id="kadin" name="cinsiyet" value="kadin">
						 <label for="kiz">kadın</label> </h1>
	<br>
	<h1>medeni hal: &nbsp; <input type="radio" id="bekar" name="medeni_hal" value="bekar" style="margin-left:50px;">
						   <label for="bekar">bekar</label>
								<input type="radio" id="evli" name="medeni_hal" value="evli">
								<label for="evli">evli</label> </h1>
	<br>
	<h1>doğum tarihi: &nbsp; <input type="date" name="dogum_tarihi" style="width:250px; height:25px; "> </h1>
	
	
	
    <input type="submit" name="kaydet" id="kaydet" value="kaydet" style="width:200px; height:60px; margin:60px 0px 0px 180px; float:left;">
    </form>

</div>











</div>









<div class="footer">


</div>











</body>
</html>