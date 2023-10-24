<?php
	
	include 'baglanti.php';

	$adi = $_POST['adi'];
	$soyadi = $_POST['soyadi'];
	$cinsiyet = $_POST['cinsiyet'];
	$medeni_hal = $_POST['medeni_hal'];
	$dogum_tarihi = $_POST['dogum_tarihi'];
	$tc_no = $_POST['tc_no'];
	$sifre = $_POST['sifre'];

	if (empty($adi) || empty($soyadi) || empty($cinsiyet) || empty($medeni_hal) || empty($dogum_tarihi) || empty($tc_no) || empty($sifre)) {
		echo '<script>alert("Lütfen tüm alanları doldurunuz!"); window.location.href = "/proje/kayit.html";</script>';
	} elseif (strlen($tc_no) != 11) {
		echo '<script>alert("TC Kimlik numarası 11 karakter olmalıdır!"); window.location.href = "/proje/kayit.html";</script>';
	} else {
		$sql = "SELECT * FROM hastabilgileri WHERE tc_no = ?";
		$params = array($tc_no);
		$stmt = sqlsrv_query($conn, $sql, $params);

		if ($stmt && sqlsrv_has_rows($stmt)) {
			// şifre boşsa update
			$row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC);
			if (empty($row['sifre'])) {
				$sql = "UPDATE hastabilgileri SET adi = ?, soyadi = ?, cinsiyet = ?, dogum_tarihi = ?, medeni_hal = ?, sifre = ? WHERE tc_no = ?";
				$params = array($adi, $soyadi, $cinsiyet, $dogum_tarihi, $medeni_hal, $sifre, $tc_no);
				$stmt = sqlsrv_query($conn, $sql, $params);
				header('Location: /proje/index.html');
				
			} else {
				echo '<script>alert("Bu kimliğe sahip bir hesap zaten var."); window.location.href = "/proje/kayit.html";</script>';
			}
		} else {
			session_destroy();
			session_start();
			$_SESSION['tc_no'] = $tc_no;

			$sql ="INSERT INTO hastabilgileri (adi, soyadi, cinsiyet, tc_no, dogum_tarihi, medeni_hal, sifre) VALUES (?, ?, ?, ?, ?, ?, ?)";
			$params = array($adi, $soyadi, $cinsiyet, $tc_no, $dogum_tarihi, $medeni_hal, $sifre);
			$stmt = sqlsrv_query($conn, $sql, $params);

			if (!$stmt) {
				die( print_r( sqlsrv_errors(), true) );
			}


        sqlsrv_close($conn);

        header('Location: /proje/index.html');
    }
}

?>