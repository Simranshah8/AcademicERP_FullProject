app.controller('BankAccountController', function ($scope, GlobalServices, $http, $timeout, $filter) {
    $scope.Title = 'BankAccount';

    $scope.LoadData = function () {
        $('.select2').select2(
            {
                allowClear: true,
                openOnEnter: true,
            }
        );
        $scope.confirmMSG = GlobalServices.getConfirmMSG();
        $scope.perPageColl = GlobalServices.getPerPageList();
        $scope.StudentSearchOptions = GlobalServices.getStudentSearchOptions();
        $scope.EmployeeSearchOptions = GlobalServices.getEmployeeSearchOptions();

        /* $scope.CountryColl = [{ id: 1, text: 'Nepal' }, { id: 2, text: 'India' }];*/
        $scope.GenderColl = [{ id: 'M', text: 'Male' }, { id: 'F', text: 'Female' }];

        $scope.NationalityColl = [{ id: 1, text: 'Nepali' }];

        $scope.InstanctCardColl = [{ id: 'I', text: 'InstantDebitCard' }, { id: 'E', text: 'NameEmbossedDebitCard' }];
        $scope.MobileBankingColl = [{ id: 10, text: 'MobBankingInquiryOnly' }, { id: 2, text: 'MobBankingInqAndTranBoth' }];
        $scope.InternetBankingColl = [{ id: 2, text: 'InternetBankingInquiryOnly' }, { id: 4, text: 'InternetBankingInqAndTranBoth' }];

        //For Occupation
        $scope.Occupationcoll = [
            { ID: 1, Occupation: 'Army' },
            { ID: 2, Occupation: 'Banker' },
            { ID: 3, Occupation: 'Businessman' },
            { ID: 4, Occupation: 'CA' },
            { ID: 5, Occupation: 'Doctor' },
            { ID: 6, Occupation: 'Engineer' },
            { ID: 7, Occupation: 'Government Employee' },
            { ID: 8, Occupation: 'Housewife' },
            { ID: 9, Occupation: 'Journalist' },
            { ID: 10, Occupation: 'Lawyer' },
            { ID: 11, Occupation: 'PILOT' },
            { ID: 12, Occupation: 'POLICE' },
            { ID: 13, Occupation: 'Politician' },
            { ID: 14, Occupation: 'Professor' },
            { ID: 15, Occupation: 'Retired' },
            { ID: 16, Occupation: 'Self Employed' },
            { ID: 17, Occupation: 'Service' },
            { ID: 18, Occupation: 'Student' },
            { ID: 19, Occupation: 'Teacher' },
            { ID: 20, Occupation: 'Unemployed' },
            { ID: 21, Occupation: 'Farmer' }
        ];

        $scope.EmployementTypeColl = [{ Id: 1, EmployementType: 'Employed' },
        { Id: 2, EmployementType: 'Self employed' },
        { Id: 3, EmployementType: 'Student' },
        { Id: 4, EmployementType: 'Unemployed' },
        { Id: 5, EmployementType: 'Housewife' },
        { Id: 6, EmployementType: 'Retired' },
        /*   { Id: 7, EmployementType: 'Other'},*/
        { Id: 8, EmployementType: 'Salaried' }
        ];


        $scope.localbodydycoll = [{ ID: 1, DistrictId: 12, LocalBody: 'SIDINGBA RURAL MUNICIPALITY' },
        { ID: 2, DistrictId: 12, LocalBody: 'MERINGDEN RURAL MUNICIPALITY' },
        { ID: 3, DistrictId: 12, LocalBody: 'MAIWAKHOLA RURAL MUNICIPALITY' },
        { ID: 4, DistrictId: 12, LocalBody: 'PHAKTANGLUNG RURAL MUNICIPALITY' },
        { ID: 5, DistrictId: 12, LocalBody: 'SIRIJANGHA RURAL MUNICIPALITY' },
        { ID: 6, DistrictId: 12, LocalBody: 'MIKWAKHOLA RURAL MUNICIPALITY' },
        //Added by bibek here
        { ID: 7, DistrictId: 12, LocalBody: 'AATHRAI TRIBENI RURAL MUNICIPALITY' },
        { ID: 8, DistrictId: 12, LocalBody: 'PATHIVARA YANGWARAK RURAL MUNICIPALITY' },
        { ID: 9, DistrictId: 12, LocalBody: 'PHUNGLING MUNICIPALITY' },
        { ID: 10, DistrictId: 9, LocalBody: 'MAKALU RURAL MUNICIPALITY' },
        { ID: 11, DistrictId: 9, LocalBody: 'CHICHILA RURAL MUNICIPALITY' },
        { ID: 12, DistrictId: 9, LocalBody: 'SILICHONG RURAL MUNICIPALITY' },
        { ID: 13, DistrictId: 9, LocalBody: 'BHOTKHOLA RURAL MUNICIPALITY' },
        { ID: 14, DistrictId: 9, LocalBody: 'SABHAPOKHARI RURAL MUNICIPALITY' },
        { ID: 15, DistrictId: 9, LocalBody: 'DHARMADEVI MUNICIPALITY' },
        { ID: 16, DistrictId: 9, LocalBody: 'MADI MUNICIPALITY' },
        { ID: 17, DistrictId: 9, LocalBody: 'PANCHAKHAPAN MUNICIPALITY' },
        { ID: 18, DistrictId: 9, LocalBody: 'CHAINPUR MUNICIPALITY' },
        { ID: 19, DistrictId: 9, LocalBody: 'KHANDBARI MUNICIPALITY' },
        { ID: 20, DistrictId: 10, LocalBody: 'SOTANG RURAL MUNICIPALITY' },
        { ID: 21, DistrictId: 10, LocalBody: 'MAHAKULUNG RURAL MUNICIPALITY' },
        { ID: 22, DistrictId: 10, LocalBody: 'LIKHUPIKE RURAL MUNICIPALITY' },
        { ID: 23, DistrictId: 10, LocalBody: 'NECHASALYAN RURAL MUNICIPALITY' },
        { ID: 24, DistrictId: 10, LocalBody: 'THULUNG DUDHKOSHI RURAL MUNICIPALITY' },
        { ID: 25, DistrictId: 10, LocalBody: 'MAAPYA DUDHKOSHI RURAL MUNICIPALITY' },
        { ID: 26, DistrictId: 10, LocalBody: 'KHUMBUPASANGLAHMU RURAL MUNICIPALITY' },
        { ID: 27, DistrictId: 10, LocalBody: 'SOLUDUDHAKUNDA MUNICIPALITY' },
        { ID: 28, DistrictId: 7, LocalBody: 'LIKHU RURAL MUNICIPALITY' },
        { ID: 29, DistrictId: 7, LocalBody: 'MOLUNG RURAL MUNICIPALITY' },
        { ID: 30, DistrictId: 7, LocalBody: 'SUNKOSHI RURAL MUNICIPALITY' },
        { ID: 31, DistrictId: 7, LocalBody: 'CHAMPADEVI RURAL MUNICIPALITY' },
        { ID: 32, DistrictId: 7, LocalBody: 'CHISANKHUGADHI RURAL MUNICIPALITY' },
        { ID: 33, DistrictId: 7, LocalBody: 'KHIJIDEMBA RURAL MUNICIPALITY' },
        { ID: 34, DistrictId: 7, LocalBody: 'MANEBHANJYANG RURAL MUNICIPALITY' },
        { ID: 35, DistrictId: 7, LocalBody: 'SIDDHICHARAN MUNICIPALITY' },
        { ID: 36, DistrictId: 5, LocalBody: 'SAKELA RURAL MUNICIPALITY' },
        { ID: 37, DistrictId: 5, LocalBody: 'KHOTEHANG RURAL MUNICIPALITY' },
        { ID: 38, DistrictId: 5, LocalBody: 'BARAHAPOKHARI RURAL MUNICIPALITY' },
        { ID: 39, DistrictId: 5, LocalBody: 'AINSELUKHARK RURAL MUNICIPALITY' },
        { ID: 40, DistrictId: 5, LocalBody: 'RAWA BESI RURAL MUNICIPALITY' },
        { ID: 41, DistrictId: 5, LocalBody: 'KEPILASAGADHI RURAL MUNICIPALITY' },
        { ID: 42, DistrictId: 5, LocalBody: 'JANTEDHUNGA RURAL MUNICIPALITY' },
        { ID: 43, DistrictId: 5, LocalBody: 'DIPRUNG CHUICHUMMA RURAL MUNICIPALITY' },
        { ID: 44, DistrictId: 5, LocalBody: 'HALESI TUWACHUNG MUNICIPALITY' },
        { ID: 45, DistrictId: 5, LocalBody: 'DIKTEL RUPAKOT MAJHUWAGADHI MUNICIPALITY' },
        { ID: 46, DistrictId: 1, LocalBody: 'ARUN RURAL MUNICIPALITY' },
        { ID: 47, DistrictId: 1, LocalBody: 'AAMCHOWK RURAL MUNICIPALITY' },
        { ID: 48, DistrictId: 1, LocalBody: 'HATUWAGADHI RURAL MUNICIPALITY' },
        { ID: 49, DistrictId: 1, LocalBody: 'PAUWADUNGMA RURAL MUNICIPALITY' },
        { ID: 50, DistrictId: 1, LocalBody: 'TEMKEMAIYUNG RURAL MUNICIPALITY' },
        { ID: 51, DistrictId: 1, LocalBody: 'SALPASILICHHO RURAL MUNICIPALITY' },
        { ID: 52, DistrictId: 1, LocalBody: 'RAMPRASAD RAI RURAL MUNICIPALITY' },
        { ID: 53, DistrictId: 1, LocalBody: 'SHADANANDA MUNICIPALITY' },
        { ID: 54, DistrictId: 1, LocalBody: 'BHOJPUR MUNICIPALITY' },
        { ID: 55, DistrictId: 2, LocalBody: 'CHAUBISE RURAL MUNICIPALITY' },
        { ID: 56, DistrictId: 2, LocalBody: 'SHAHIDBHUMI RURAL MUNICIPALITY' },
        { ID: 57, DistrictId: 2, LocalBody: 'SANGURIGADHI RURAL MUNICIPALITY' },
        { ID: 58, DistrictId: 2, LocalBody: 'CHHATHAR JORPATI RURAL MUNICIPALITY' },
        { ID: 59, DistrictId: 2, LocalBody: 'PAKHRIBAS MUNICIPALITY' },
        { ID: 60, DistrictId: 2, LocalBody: 'MAHALAXMI MUNICIPALITY' },
        { ID: 61, DistrictId: 2, LocalBody: 'DHANKUTA MUNICIPALITY' },
        { ID: 62, DistrictId: 13, LocalBody: 'CHHATHAR RURAL MUNICIPALITY' },
        { ID: 63, DistrictId: 13, LocalBody: 'PHEDAP RURAL MUNICIPALITY' },
        { ID: 64, DistrictId: 13, LocalBody: 'AATHRAI RURAL MUNICIPALITY' },
        { ID: 65, DistrictId: 13, LocalBody: 'MENCHAYAM RURAL MUNICIPALITY' },
        { ID: 66, DistrictId: 13, LocalBody: 'LALIGURANS MUNICIPALITY' },
        { ID: 67, DistrictId: 13, LocalBody: 'MYANGLUNG MUNICIPALITY' },

        { ID: 68, DistrictId: 8, LocalBody: 'YANGWARAK RURAL MUNICIPALITY' },
        { ID: 69, DistrictId: 8, LocalBody: 'HILIHANG RURAL MUNICIPALITY' },
        { ID: 70, DistrictId: 8, LocalBody: 'FALELUNG RURAL MUNICIPALITY' },
        { ID: 71, DistrictId: 8, LocalBody: 'TUMBEWA RURAL MUNICIPALITY' },
        { ID: 72, DistrictId: 8, LocalBody: 'KUMMAYAK RURAL MUNICIPALITY' },
        { ID: 73, DistrictId: 8, LocalBody: 'MIKLAJUNG RURAL MUNICIPALITY' },
        { ID: 74, DistrictId: 8, LocalBody: 'FALGUNANDA RURAL MUNICIPALITY' },
        { ID: 75, DistrictId: 8, LocalBody: 'PHIDIM MUNICIPALITY' },
        { ID: 76, DistrictId: 3, LocalBody: 'RONG RURAL MUNICIPALITY' },
        { ID: 77, DistrictId: 3, LocalBody: 'MANGSEBUNG RURAL MUNICIPALITY' },
        { ID: 78, DistrictId: 3, LocalBody: 'CHULACHULI RURAL MUNICIPALITY' },
        { ID: 79, DistrictId: 3, LocalBody: 'SANDAKPUR RURAL MUNICIPALITY' },
        { ID: 80, DistrictId: 3, LocalBody: 'FAKPHOKTHUM RURAL MUNICIPALITY' },
        { ID: 81, DistrictId: 3, LocalBody: 'MAIJOGMAI RURAL MUNICIPALITY' },
        { ID: 82, DistrictId: 3, LocalBody: 'ILLAM MUNICIPALITY' },
        { ID: 83, DistrictId: 3, LocalBody: 'MAI MUNICIPALITY' },
        { ID: 84, DistrictId: 3, LocalBody: 'DEUMAI MUNICIPALITY' },
        { ID: 85, DistrictId: 3, LocalBody: 'SURYODAYA MUNICIPALITY' },
        { ID: 86, DistrictId: 4, LocalBody: 'KAMAL RURAL MUNICIPALITY' },
        { ID: 87, DistrictId: 4, LocalBody: 'JHAPA RURAL MUNICIPALITY' },
        { ID: 88, DistrictId: 4, LocalBody: 'KACHANKAWAL RURAL MUNICIPALITY' },
        { ID: 89, DistrictId: 4, LocalBody: 'GAURIGANJ RURAL MUNICIPALITY' },
        { ID: 90, DistrictId: 4, LocalBody: 'BARHADASHI RURAL MUNICIPALITY' },
        { ID: 91, DistrictId: 4, LocalBody: 'HALDIBARI RURAL MUNICIPALITY' },
        { ID: 92, DistrictId: 4, LocalBody: 'BUDDHASHANTI RURAL MUNICIPALITY' },
        { ID: 93, DistrictId: 4, LocalBody: 'SHIVASATAXI MUNICIPALITY' },
        { ID: 94, DistrictId: 4, LocalBody: 'BHADRAPUR MUNICIPALITY' },
        { ID: 95, DistrictId: 4, LocalBody: 'KANKAI MUNICIPALITY' },
        { ID: 96, DistrictId: 4, LocalBody: 'BIRTAMOD MUNICIPALITY' },
        { ID: 97, DistrictId: 4, LocalBody: 'MECHINAGAR MUNICIPALITY' },
        { ID: 98, DistrictId: 4, LocalBody: 'DAMAK MUNICIPALITY' },
        { ID: 99, DistrictId: 4, LocalBody: 'ARJUNDHARA MUNICIPALITY' },
        { ID: 100, DistrictId: 4, LocalBody: 'GAURADHAHA MUNICIPALITY' },

        { ID: 101, DistrictId: 6, LocalBody: 'JAHADA RURAL MUNICIPALITY' },
        { ID: 102, DistrictId: 6, LocalBody: 'KATAHARI RURAL MUNICIPALITY' },
        { ID: 103, DistrictId: 6, LocalBody: 'GRAMTHAN RURAL MUNICIPALITY' },
        { ID: 104, DistrictId: 6, LocalBody: 'DHANPALTHAN RURAL MUNICIPALITY' },
        { ID: 105, DistrictId: 6, LocalBody: 'KERABARI RURAL MUNICIPALITY' },
        { ID: 106, DistrictId: 6, LocalBody: 'BUDHIGANGA RURAL MUNICIPALITY' },
        { ID: 107, DistrictId: 6, LocalBody: 'KANEPOKHARI RURAL MUNICIPALITY' },
        { ID: 108, DistrictId: 6, LocalBody: 'MIKLAJUNG RURAL MUNICIPALITY' },
        { ID: 109, DistrictId: 6, LocalBody: 'LETANG MUNICIPALITY' },
        { ID: 110, DistrictId: 6, LocalBody: 'SUNWARSHI MUNICIPALITY' },
        { ID: 111, DistrictId: 6, LocalBody: 'RANGELI MUNICIPALITY' },
        { ID: 112, DistrictId: 6, LocalBody: 'PATAHRISHANISHCHARE MUNICIPALITY' },
        { ID: 113, DistrictId: 6, LocalBody: 'BIRATNAGAR METROPOLITIAN CITY' },
        { ID: 114, DistrictId: 6, LocalBody: 'URALABARI MUNICIPALITY' },
        { ID: 115, DistrictId: 6, LocalBody: 'BELBARI MUNICIPALITY' },

        { ID: 116, DistrictId: 6, LocalBody: 'SUNDARHARAICHA MUNICIPALITY' },
        { ID: 117, DistrictId: 6, LocalBody: 'RATUWAMAI MUNICIPALITY' },
        { ID: 118, DistrictId: 11, LocalBody: 'GADHI RURAL MUNICIPALITY' },
        { ID: 119, DistrictId: 11, LocalBody: 'KOSHI RURAL MUNICIPALITY' },
        { ID: 120, DistrictId: 11, LocalBody: 'BARJU RURAL MUNICIPALITY' },
        { ID: 121, DistrictId: 11, LocalBody: 'HARINAGAR RURAL MUNICIPALITY' },
        { ID: 122, DistrictId: 11, LocalBody: 'DEWANGANJ RURAL MUNICIPALITY' },
        { ID: 123, DistrictId: 11, LocalBody: 'BHOKRAHA NARSING RURAL MUNICIPALITY' },
        { ID: 124, DistrictId: 11, LocalBody: 'RAMDHUNI MUNICIPALITY' },
        { ID: 125, DistrictId: 11, LocalBody: 'BARAHCHHETRA MUNICIPALITY' },
        { ID: 126, DistrictId: 11, LocalBody: 'BARAHCHHETRA MUNICIPALITY' },
        { ID: 127, DistrictId: 11, LocalBody: 'INARUWA MUNICIPALITY' },
        { ID: 128, DistrictId: 11, LocalBody: 'DHARAN SUB-METROPOLITIAN CITY' },
        { ID: 129, DistrictId: 11, LocalBody: 'ITAHARI SUB-METROPOLITIAN CITY' },
        { ID: 130, DistrictId: 14, LocalBody: 'TAPLI RURAL MUNICIPALITY' },
        { ID: 131, DistrictId: 14, LocalBody: 'RAUTAMAI RURAL MUNICIPALITY' },

        { ID: 132, DistrictId: 14, LocalBody: 'UDAYAPURGADHI RURAL MUNICIPALITY' },
        { ID: 133, DistrictId: 14, LocalBody: 'LIMCHUNGBUNG RURAL MUNICIPALITY' },
        { ID: 134, DistrictId: 14, LocalBody: 'CHAUDANDIGADHI MUNICIPALITY' },
        { ID: 135, DistrictId: 14, LocalBody: 'TRIYUGA MUNICIPALITY' },
        { ID: 136, DistrictId: 14, LocalBody: 'KATARI MUNICIPALITY' },
        { ID: 137, DistrictId: 14, LocalBody: 'BELAKA MUNICIPALITY' },
        { ID: 138, DistrictId: 20, LocalBody: 'RAJGADH RURAL MUNICIPALITY' },
        { ID: 139, DistrictId: 20, LocalBody: 'RUPANI RURAL MUNICIPALITY' },
        { ID: 140, DistrictId: 20, LocalBody: 'TIRAHUT RURAL MUNICIPALITY' },
        { ID: 141, DistrictId: 20, LocalBody: 'MAHADEVA RURAL MUNICIPALITY' },
        { ID: 142, DistrictId: 20, LocalBody: 'BISHNUPUR RURAL MUNICIPALITY' },
        { ID: 143, DistrictId: 20, LocalBody: 'CHHINNAMASTA RURAL MUNICIPALITY' },
        { ID: 144, DistrictId: 20, LocalBody: 'BALAN BIHUL RURAL MUNICIPALITY' },
        { ID: 145, DistrictId: 20, LocalBody: 'TILATHI KOILADI RURAL MUNICIPALITY' },
        { ID: 146, DistrictId: 20, LocalBody: 'AGNISAIR KRISHNA SAVARAN RURAL MUNICIPALITY' },
        { ID: 147, DistrictId: 20, LocalBody: 'HANUMANNAGAR KANKALINI MUNICIPALITY' },

        { ID: 148, DistrictId: 20, LocalBody: 'KANCHANRUP MUNICIPALITY' },
        { ID: 149, DistrictId: 20, LocalBody: 'RAJBIRAJ MUNICIPALITY' },
        { ID: 150, DistrictId: 20, LocalBody: 'KHADAK MUNICIPALITY' },
        { ID: 151, DistrictId: 20, LocalBody: 'DAKNESHWORI MUNICIPALITY' },
        { ID: 152, DistrictId: 20, LocalBody: 'SAPTAKOSHI RURAL MUNICIPALITY' },
        { ID: 153, DistrictId: 20, LocalBody: 'SURUNGA MUNICIPALITY' },
        { ID: 154, DistrictId: 20, LocalBody: 'SHAMBHUNATH MUNICIPALITY' },
        { ID: 155, DistrictId: 20, LocalBody: 'BODE BARSAIN MUNICIPALITY' },
        { ID: 156, DistrictId: 22, LocalBody: 'AURAHI RURAL MUNICIPALITY' },
        { ID: 157, DistrictId: 22, LocalBody: 'NARAHA RURAL MUNICIPALITY' },
        { ID: 158, DistrictId: 22, LocalBody: 'ARNAMA RURAL MUNICIPALITY' },
        { ID: 159, DistrictId: 22, LocalBody: 'BHAGAWANPUR RURAL MUNICIPALITY' },
        { ID: 160, DistrictId: 22, LocalBody: 'NAWARAJPUR RURAL MUNICIPALITY' },
        { ID: 161, DistrictId: 22, LocalBody: 'BISHNUPUR RURAL MUNICIPALITY' },
        { ID: 162, DistrictId: 22, LocalBody: 'BARIYARPATTI RURAL MUNICIPALITY' },
        { ID: 163, DistrictId: 22, LocalBody: 'LAXMIPUR PATARI RURAL MUNICIPALITY' },
        { ID: 164, DistrictId: 22, LocalBody: 'SAKHUWANANKARKATTI RURAL MUNICIPALITY' },
        { ID: 165, DistrictId: 22, LocalBody: 'MIRCHAIYA MUNICIPALITY' },
        { ID: 166, DistrictId: 22, LocalBody: 'LAHAN MUNICIPALITY' },
        { ID: 167, DistrictId: 22, LocalBody: 'SIRAHA MUNICIPALITY' },
        { ID: 168, DistrictId: 22, LocalBody: 'DHANGADHIMAI MUNICIPALITY' },
        { ID: 169, DistrictId: 22, LocalBody: 'KALYANPUR MUNICIPALITY' },
        { ID: 170, DistrictId: 22, LocalBody: 'KARJANHA MUNICIPALITY' },
        { ID: 171, DistrictId: 22, LocalBody: 'GOLBAZAR MUNICIPALITY' },
        { ID: 172, DistrictId: 22, LocalBody: 'SUKHIPUR MUNICIPALITY' },
        { ID: 173, DistrictId: 16, LocalBody: 'AAURAHI RURAL MUNICIPALITY' },
        { ID: 174, DistrictId: 16, LocalBody: 'DHANAUJI RURAL MUNICIPALITY' },
        { ID: 175, DistrictId: 16, LocalBody: 'BATESHWOR RURAL MUNICIPALITY' },
        { ID: 176, DistrictId: 16, LocalBody: 'JANAKNANDANI RURAL MUNICIPALITY' },
        { ID: 177, DistrictId: 16, LocalBody: 'LAKSHMINIYA RURAL MUNICIPALITY' },

        { ID: 178, DistrictId: 16, LocalBody: 'MUKHIYAPATTI MUSARMIYA RURAL MUNICIPALITY' },
        { ID: 179, DistrictId: 16, LocalBody: 'MITHILA BIHARI MUNICIPALITY' },
        { ID: 180, DistrictId: 16, LocalBody: 'KAMALA MUNICIPALITY' },
        { ID: 181, DistrictId: 16, LocalBody: 'NAGARAIN MUNICIPALITY' },
        { ID: 182, DistrictId: 16, LocalBody: 'GANESHMAN CHARNATH MUNICIPALITY' },
        { ID: 183, DistrictId: 16, LocalBody: 'MITHILA MUNICIPALITY' },
        { ID: 184, DistrictId: 16, LocalBody: 'DHANUSADHAM MUNICIPALITY' },
        { ID: 185, DistrictId: 16, LocalBody: 'BIDEHA MUNICIPALITY' },
        { ID: 186, DistrictId: 16, LocalBody: 'SABAILA MUNICIPALITY' },
        { ID: 187, DistrictId: 16, LocalBody: 'HANSAPUR MUNICIPALITY' },
        { ID: 188, DistrictId: 16, LocalBody: 'JANAKPURDHAM SUB-METROPOLITIAN CITY' },
        { ID: 189, DistrictId: 16, LocalBody: 'SAHIDNAGAR MUNICIPALITY' },

        { ID: 190, DistrictId: 16, LocalBody: 'CHHIRESHWORNATH MUNICIPALITY' },
        { ID: 191, DistrictId: 17, LocalBody: 'PIPRA RURAL MUNICIPALITY' },
        { ID: 192, DistrictId: 17, LocalBody: 'SONAMA RURAL MUNICIPALITY' },
        { ID: 193, DistrictId: 17, LocalBody: 'SAMSI RURAL MUNICIPALITY' },
        { ID: 194, DistrictId: 17, LocalBody: 'EKDANRA RURAL MUNICIPALITY' },
        { ID: 195, DistrictId: 17, LocalBody: 'MAHOTTARI RURAL MUNICIPALITY' },
        { ID: 196, DistrictId: 17, LocalBody: 'GAUSHALA MUNICIPALITY' },
        { ID: 197, DistrictId: 17, LocalBody: 'RAMGOPALPUR MUNICIPALITY' },
        { ID: 198, DistrictId: 17, LocalBody: 'AURAHI MUNICIPALITY' },

        { ID: 199, DistrictId: 17, LocalBody: 'BARDIBAS MUNICIPALITY' },
        { ID: 200, DistrictId: 17, LocalBody: 'BHANGAHA MUNICIPALITY' },
        { ID: 201, DistrictId: 17, LocalBody: 'JALESWOR MUNICIPALITY' },
        { ID: 202, DistrictId: 17, LocalBody: 'BALWA MUNICIPALITY' },
        { ID: 203, DistrictId: 17, LocalBody: 'MANRA SISWA MUNICIPALITY' },
        { ID: 204, DistrictId: 17, LocalBody: 'MATIHANI MUNICIPALITY' },
        { ID: 205, DistrictId: 17, LocalBody: 'LOHARPATTI MUNICIPALITY' },
        { ID: 206, DistrictId: 21, LocalBody: 'DHANKAUL RURAL MUNICIPALITY' },
        { ID: 207, DistrictId: 21, LocalBody: 'PARSA RURAL MUNICIPALITY' },

        { ID: 208, DistrictId: 21, LocalBody: 'BISHNU RURAL MUNICIPALITY' },
        { ID: 209, DistrictId: 21, LocalBody: 'RAMNAGAR RURAL MUNICIPALITY' },
        { ID: 210, DistrictId: 21, LocalBody: 'KAUDENA RURAL MUNICIPALITY' },
        { ID: 211, DistrictId: 21, LocalBody: 'BASBARIYA RURAL MUNICIPALITY' },
        { ID: 212, DistrictId: 21, LocalBody: 'CHANDRANAGAR RURAL MUNICIPALITY' },
        { ID: 213, DistrictId: 21, LocalBody: 'CHAKRAGHATTA RURAL MUNICIPALITY' },
        { ID: 214, DistrictId: 21, LocalBody: 'BRAMHAPURI RURAL MUNICIPALITY' },
        { ID: 215, DistrictId: 21, LocalBody: 'BARAHATHAWA MUNICIPALITY' },
        { ID: 216, DistrictId: 21, LocalBody: 'HARIPUR MUNICIPALITY' },
        { ID: 217, DistrictId: 21, LocalBody: 'ISHWORPUR MUNICIPALITY' },
        { ID: 218, DistrictId: 21, LocalBody: 'LALBANDI MUNICIPALITY' },
        { ID: 219, DistrictId: 21, LocalBody: 'MALANGAWA MUNICIPALITY' },
        { ID: 220, DistrictId: 21, LocalBody: 'KABILASI MUNICIPALITY' },
        { ID: 221, DistrictId: 21, LocalBody: 'BAGMATI MUNICIPALITY' },
        { ID: 222, DistrictId: 21, LocalBody: 'HARIWAN MUNICIPALITY' },
        { ID: 223, DistrictId: 21, LocalBody: 'BALARA MUNICIPALITY' },
        { ID: 224, DistrictId: 21, LocalBody: 'HARIPURWA MUNICIPALITY' },
        { ID: 225, DistrictId: 21, LocalBody: 'GODAITA MUNICIPALITY' },
        { ID: 226, DistrictId: 19, LocalBody: 'YEMUNAMAI RURAL MUNICIPALITY' },
        { ID: 227, DistrictId: 19, LocalBody: 'DURGA BHAGWATI RURAL MUNICIPALITY' },
        { ID: 228, DistrictId: 19, LocalBody: 'KATAHARIYA MUNICIPALITY' },
        { ID: 229, DistrictId: 19, LocalBody: 'MAULAPUR MUNICIPALITY' },
        { ID: 230, DistrictId: 19, LocalBody: 'MADHAV NARAYAN MUNICIPALITY' },
        { ID: 231, DistrictId: 19, LocalBody: 'GAUR MUNICIPALITY' },
        { ID: 232, DistrictId: 19, LocalBody: 'GUJARA MUNICIPALITY' },
        { ID: 233, DistrictId: 19, LocalBody: 'GARUDA MUNICIPALITY' },
        { ID: 234, DistrictId: 19, LocalBody: 'ISHANATH MUNICIPALITY' },
        { ID: 235, DistrictId: 19, LocalBody: 'CHANDRAPUR MUNICIPALITY' },
        { ID: 236, DistrictId: 19, LocalBody: 'DEWAHHI GONAHI MUNICIPALITY' },
        { ID: 237, DistrictId: 19, LocalBody: 'BRINDABAN MUNICIPALITY' },
        { ID: 238, DistrictId: 19, LocalBody: 'RAJPUR MUNICIPALITY' },
        { ID: 239, DistrictId: 19, LocalBody: 'RAJDEVI MUNICIPALITY' },
        { ID: 240, DistrictId: 19, LocalBody: 'GADHIMAI MUNICIPALITY' },
        { ID: 241, DistrictId: 19, LocalBody: 'PHATUWA BIJAYAPUR MUNICIPALITY' },
        { ID: 242, DistrictId: 19, LocalBody: 'BAUDHIMAI MUNICIPALITY' },
        { ID: 243, DistrictId: 19, LocalBody: 'PAROHA MUNICIPALITY' },

        { ID: 244, DistrictId: 15, LocalBody: 'PHETA RURAL MUNICIPALITY' },
        { ID: 245, DistrictId: 15, LocalBody: 'DEVTAL RURAL MUNICIPALITY' },
        { ID: 246, DistrictId: 15, LocalBody: 'PRASAUNI RURAL MUNICIPALITY' },
        { ID: 247, DistrictId: 15, LocalBody: 'SUWARNA RURAL MUNICIPALITY' },
        { ID: 248, DistrictId: 15, LocalBody: 'BARAGADHI RURAL MUNICIPALITY' },
        { ID: 249, DistrictId: 15, LocalBody: 'KARAIYAMAI RURAL MUNICIPALITY' },
        { ID: 250, DistrictId: 15, LocalBody: 'PARWANIPUR RURAL MUNICIPALITY' },
        { ID: 251, DistrictId: 15, LocalBody: 'BISHRAMPUR RURAL MUNICIPALITY' },
        { ID: 252, DistrictId: 15, LocalBody: 'ADARSHKOTWAL RURAL MUNICIPALITY' },
        { ID: 253, DistrictId: 15, LocalBody: 'JITPUR SIMARA SUB-METROPOLITIAN CITY' },
        { ID: 254, DistrictId: 15, LocalBody: 'KALAIYA SUB-METROPOLITIAN CITY' },
        { ID: 255, DistrictId: 15, LocalBody: 'PACHARAUTA MUNICIPALITY' },
        { ID: 256, DistrictId: 15, LocalBody: 'NIJGADH MUNICIPALITY' },
        { ID: 257, DistrictId: 15, LocalBody: 'SIMRAUNGADH MUNICIPALITY' },
        { ID: 258, DistrictId: 15, LocalBody: 'MAHAGADHIMAI MUNICIPALITY' },
        { ID: 259, DistrictId: 15, LocalBody: 'KOLHABI MUNICIPALITY' },
        { ID: 260, DistrictId: 18, LocalBody: 'THORI RURAL MUNICIPALITY' },
        { ID: 261, DistrictId: 18, LocalBody: 'DHOBINI RURAL MUNICIPALITY' },
        { ID: 262, DistrictId: 18, LocalBody: 'CHHIPAHARMAI RURAL MUNICIPALITY' },
        { ID: 263, DistrictId: 18, LocalBody: 'JIRABHAWANI RURAL MUNICIPALITY' },
        { ID: 264, DistrictId: 18, LocalBody: 'JAGARNATHPUR RURAL MUNICIPALITY' },
        { ID: 265, DistrictId: 18, LocalBody: 'KALIKAMAI RURAL MUNICIPALITY' },
        { ID: 266, DistrictId: 18, LocalBody: 'BINDABASINI RURAL MUNICIPALITY' },
        { ID: 267, DistrictId: 18, LocalBody: 'PAKAHAMAINPUR RURAL MUNICIPALITY' },
        { ID: 268, DistrictId: 18, LocalBody: 'SAKHUWAPRASAUNI RURAL MUNICIPALITY' },
        { ID: 269, DistrictId: 18, LocalBody: 'PATERWASUGAULI RURAL MUNICIPALITY' },
        { ID: 270, DistrictId: 18, LocalBody: 'BIRGUNJ METROPOLITIAN CITY' },
        { ID: 271, DistrictId: 18, LocalBody: 'BAHUDARAMAI MUNICIPALITY' },
        { ID: 272, DistrictId: 18, LocalBody: 'POKHARIYA MUNICIPALITY' },
        { ID: 273, DistrictId: 18, LocalBody: 'PARSAGADHI MUNICIPALITY' },
        { ID: 274, DistrictId: 60, LocalBody: 'BHUME RURAL MUNICIPALITY' },
        { ID: 275, DistrictId: 57, LocalBody: 'MADI RURAL MUNICIPALITY' },
        { ID: 276, DistrictId: 57, LocalBody: 'THAWANG RURAL MUNICIPALITY' },
        { ID: 277, DistrictId: 57, LocalBody: 'SUNCHHAHARI RURAL MUNICIPALITY' },
        { ID: 278, DistrictId: 57, LocalBody: 'LUNGRI RURAL MUNICIPALITY' },
        { ID: 279, DistrictId: 57, LocalBody: 'GANGADEV RURAL MUNICIPALITY' },
        { ID: 280, DistrictId: 57, LocalBody: 'TRIBENI RURAL MUNICIPALITY' },
        { ID: 281, DistrictId: 57, LocalBody: 'PARIWARTAN RURAL MUNICIPALITY' },
        { ID: 282, DistrictId: 57, LocalBody: 'RUNTIGADI RURAL MUNICIPALITY' },
        { ID: 283, DistrictId: 57, LocalBody: 'SUNIL SMRITI RURAL MUNICIPALITY' },
        { ID: 284, DistrictId: 57, LocalBody: 'ROLPA MUNICIPALITY' },

        { ID: 285, DistrictId: 56, LocalBody: 'AYIRABATI RURAL MUNICIPALITY' },
        { ID: 286, DistrictId: 56, LocalBody: 'GAUMUKHI RURAL MUNICIPALITY' },
        { ID: 287, DistrictId: 56, LocalBody: 'JHIMRUK RURAL MUNICIPALITY' },
        { ID: 288, DistrictId: 56, LocalBody: 'NAUBAHINI RURAL MUNICIPALITY' },
        { ID: 289, DistrictId: 56, LocalBody: 'MANDAVI RURAL MUNICIPALITY' },
        { ID: 290, DistrictId: 56, LocalBody: 'MALLARANI RURAL MUNICIPALITY' },
        { ID: 291, DistrictId: 56, LocalBody: 'SARUMARANI RURAL MUNICIPALITY' },
        { ID: 292, DistrictId: 56, LocalBody: 'PYUTHAN MUNICIPALITY' },
        { ID: 293, DistrictId: 56, LocalBody: 'SWORGADWARY MUNICIPALITY' },
        { ID: 294, DistrictId: 53, LocalBody: 'RURU RURAL MUNICIPALITY' },
        { ID: 295, DistrictId: 53, LocalBody: 'ISMA RURAL MUNICIPALITY' },
        { ID: 296, DistrictId: 53, LocalBody: 'MADANE RURAL MUNICIPALITY' },
        { ID: 297, DistrictId: 53, LocalBody: 'MALIKA RURAL MUNICIPALITY' },
        { ID: 298, DistrictId: 53, LocalBody: 'CHATRAKOT RURAL MUNICIPALITY' },
        { ID: 299, DistrictId: 53, LocalBody: 'DHURKOT RURAL MUNICIPALITY' },
        { ID: 300, DistrictId: 53, LocalBody: 'SATYAWATI RURAL MUNICIPALITY' },

        { ID: 301, DistrictId: 53, LocalBody: 'CHANDRAKOT RURAL MUNICIPALITY' },
        { ID: 302, DistrictId: 53, LocalBody: 'KALIGANDAKI RURAL MUNICIPALITY' },
        { ID: 303, DistrictId: 53, LocalBody: 'GULMIDARBAR RURAL MUNICIPALITY' },

        { ID: 304, DistrictId: 53, LocalBody: 'RESUNGA MUNICIPALITY' },
        { ID: 305, DistrictId: 53, LocalBody: 'MUSIKOT MUNICIPALITY' },
        { ID: 306, DistrictId: 49, LocalBody: 'PANINI RURAL MUNICIPALITY' },
        { ID: 307, DistrictId: 49, LocalBody: 'CHHATRADEV RURAL MUNICIPALITY' },
        { ID: 308, DistrictId: 49, LocalBody: 'MALARANI RURAL MUNICIPALITY' },
        { ID: 309, DistrictId: 49, LocalBody: 'BHUMEKASTHAN MUNICIPALITY' },
        { ID: 310, DistrictId: 49, LocalBody: 'SITGANGA MUNICIPALITY' },
        { ID: 311, DistrictId: 49, LocalBody: 'SANDHIKHARKA MUNICIPALITY' },
        { ID: 312, DistrictId: 55, LocalBody: 'RAMBHA RURAL MUNICIPALITY' },
        { ID: 313, DistrictId: 55, LocalBody: 'TINAU RURAL MUNICIPALITY' },
        { ID: 314, DistrictId: 55, LocalBody: 'NISDI RURAL MUNICIPALITY' },
        { ID: 315, DistrictId: 55, LocalBody: 'MATHAGADHI RURAL MUNICIPALITY' },
        { ID: 316, DistrictId: 55, LocalBody: 'RIBDIKOT RURAL MUNICIPALITY' },
        { ID: 317, DistrictId: 55, LocalBody: 'PURBAKHOLA RURAL MUNICIPALITY' },
        { ID: 318, DistrictId: 55, LocalBody: 'BAGNASKALI RURAL MUNICIPALITY' },
        { ID: 319, DistrictId: 55, LocalBody: 'RAINADEVI CHHAHARA RURAL MUNICIPALITY' },
        { ID: 320, DistrictId: 54, LocalBody: 'TANSEN MUNICIPALITY' },
        { ID: 321, DistrictId: 54, LocalBody: 'RAMPUR MUNICIPALITY' },
        { ID: 322, DistrictId: 48, LocalBody: 'SARAWAL RURAL MUNICIPALITY' },
        { ID: 323, DistrictId: 48, LocalBody: 'SUSTA RURAL MUNICIPALITY' },
        { ID: 324, DistrictId: 48, LocalBody: 'PRATAPPUR RURAL MUNICIPALITY' },
        { ID: 325, DistrictId: 48, LocalBody: 'PALHI NANDAN RURAL MUNICIPALITY' },
        { ID: 326, DistrictId: 48, LocalBody: 'BARDAGHAT MUNICIPALITY' },
        { ID: 327, DistrictId: 48, LocalBody: 'SUNWAL MUNICIPALITY' },
        { ID: 328, DistrictId: 48, LocalBody: 'RAMGRAM MUNICIPALITY' },
        { ID: 329, DistrictId: 59, LocalBody: 'KANCHAN RURAL MUNICIPALITY' },
        { ID: 330, DistrictId: 59, LocalBody: 'SIYARI RURAL MUNICIPALITY' },
        { ID: 331, DistrictId: 59, LocalBody: 'ROHINI RURAL MUNICIPALITY' },
        { ID: 332, DistrictId: 59, LocalBody: 'GAIDAHAWA RURAL MUNICIPALITY' },
        { ID: 333, DistrictId: 59, LocalBody: 'OMSATIYA RURAL MUNICIPALITY' },
        { ID: 334, DistrictId: 59, LocalBody: 'SUDHDHODHAN RURAL MUNICIPALITY' },

        { ID: 335, DistrictId: 59, LocalBody: 'MAYADEVI RURAL MUNICIPALITY' },
        { ID: 336, DistrictId: 59, LocalBody: 'MARCHAWARI RURAL MUNICIPALITY' },
        { ID: 337, DistrictId: 59, LocalBody: 'KOTAHIMAI RURAL MUNICIPALITY' },

        { ID: 338, DistrictId: 59, LocalBody: 'SAMMARIMAI RURAL MUNICIPALITY' },
        { ID: 339, DistrictId: 59, LocalBody: 'BUTWAL SUB-METROPOLITIAN CITY' },
        { ID: 340, DistrictId: 59, LocalBody: 'LUMBINI SANSKRITIK MUNICIPALITY' },
        { ID: 341, DistrictId: 59, LocalBody: 'DEVDAHA MUNICIPALITY' },
        { ID: 342, DistrictId: 59, LocalBody: 'SAINAMAINA MUNICIPALITY' },
        { ID: 343, DistrictId: 59, LocalBody: 'SIDDHARTHANAGAR MUNICIPALITY' },
        { ID: 344, DistrictId: 59, LocalBody: 'TILLOTAMA MUNICIPALITY' },
        { ID: 345, DistrictId: 54, LocalBody: 'YASHODHARA RURAL MUNICIPALITY' },
        { ID: 346, DistrictId: 54, LocalBody: 'BIJAYANAGAR RURAL MUNICIPALITY' },
        { ID: 347, DistrictId: 54, LocalBody: 'MAYADEVI RURAL MUNICIPALITY' },
        { ID: 348, DistrictId: 54, LocalBody: 'SUDDHODHAN RURAL MUNICIPALITY' },
        { ID: 349, DistrictId: 54, LocalBody: 'SHIVARAJ MUNICIPALITY' },
        { ID: 350, DistrictId: 54, LocalBody: 'KAPILBASTU MUNICIPALITY' },
        { ID: 351, DistrictId: 54, LocalBody: 'BUDDHABHUMI MUNICIPALITY' },
        { ID: 352, DistrictId: 54, LocalBody: 'MAHARAJGUNJ MUNICIPALITY' },
        { ID: 353, DistrictId: 54, LocalBody: 'BANGANGA MUNICIPALITY' },
        { ID: 354, DistrictId: 54, LocalBody: 'KRISHNANAGAR MUNICIPALITY' },
        { ID: 355, DistrictId: 52, LocalBody: 'BABAI RURAL MUNICIPALITY' },
        { ID: 356, DistrictId: 52, LocalBody: 'GADHAWA RURAL MUNICIPALITY' },
        { ID: 357, DistrictId: 52, LocalBody: 'RAPTI RURAL MUNICIPALITY' },
        { ID: 358, DistrictId: 52, LocalBody: 'RAJPUR RURAL MUNICIPALITY' },
        { ID: 359, DistrictId: 52, LocalBody: 'DANGISHARAN RURAL MUNICIPALITY' },
        { ID: 360, DistrictId: 52, LocalBody: 'SHANTINAGAR RURAL MUNICIPALITY' },
        { ID: 361, DistrictId: 52, LocalBody: 'BANGLACHULI RURAL MUNICIPALITY' },
        { ID: 362, DistrictId: 52, LocalBody: 'TULSIPUR SUB-METROPOLITIAN CITY' },
        { ID: 363, DistrictId: 52, LocalBody: 'GHORAHI SUB-METROPOLITIAN CITY' },
        { ID: 364, DistrictId: 52, LocalBody: 'LAMAHI MUNICIPALITY' },
        { ID: 365, DistrictId: 50, LocalBody: 'KHAJURA RURAL MUNICIPALITY' },
        { ID: 366, DistrictId: 50, LocalBody: 'JANKI RURAL MUNICIPALITY' },
        { ID: 367, DistrictId: 50, LocalBody: 'BAIJANATH RURAL MUNICIPALITY' },
        { ID: 368, DistrictId: 50, LocalBody: 'DUDUWA RURAL MUNICIPALITY' },
        { ID: 369, DistrictId: 50, LocalBody: 'NARAINAPUR RURAL MUNICIPALITY' },
        { ID: 370, DistrictId: 50, LocalBody: 'RAPTI SONARI RURAL MUNICIPALITY' },
        { ID: 371, DistrictId: 50, LocalBody: 'KOHALPUR MUNICIPALITY' },
        { ID: 372, DistrictId: 50, LocalBody: 'NEPALGUNJ SUB-METROPOLITIAN CITY' },
        { ID: 373, DistrictId: 51, LocalBody: 'GERUWA RURAL MUNICIPALITY' },
        { ID: 374, DistrictId: 51, LocalBody: 'BADHAIYATAL RURAL MUNICIPALITY' },
        { ID: 375, DistrictId: 51, LocalBody: 'THAKURBABA MUNICIPALITY' },
        { ID: 376, DistrictId: 51, LocalBody: 'BANSAGADHI MUNICIPALITY' },
        { ID: 377, DistrictId: 51, LocalBody: 'BARBARDIYA MUNICIPALITY' },
        { ID: 378, DistrictId: 51, LocalBody: 'RAJAPUR MUNICIPALITY' },
        { ID: 379, DistrictId: 51, LocalBody: 'MADHUWAN MUNICIPALITY' },
        { ID: 380, DistrictId: 51, LocalBody: 'GULARIYA MUNICIPALITY' },
        { ID: 381, DistrictId: 26, LocalBody: 'BIGU RURAL MUNICIPALITY' },
        { ID: 382, DistrictId: 26, LocalBody: 'SAILUNG RURAL MUNICIPALITY' },
        { ID: 383, DistrictId: 26, LocalBody: 'MELUNG RURAL MUNICIPALITY' },
        { ID: 384, DistrictId: 26, LocalBody: 'BAITESHWOR RURAL MUNICIPALITY' },
        { ID: 385, DistrictId: 26, LocalBody: 'TAMAKOSHI RURAL MUNICIPALITY' },
        { ID: 386, DistrictId: 26, LocalBody: 'GAURISHANKAR RURAL MUNICIPALITY' },
        { ID: 387, DistrictId: 26, LocalBody: 'KALINCHOK RURAL MUNICIPALITY' },
        { ID: 388, DistrictId: 26, LocalBody: 'JIRI MUNICIPALITY' },
        { ID: 389, DistrictId: 26, LocalBody: 'BHIMESHWOR MUNICIPALITY' },
        { ID: 390, DistrictId: 34, LocalBody: 'JUGAL RURAL MUNICIPALITY' },
        { ID: 391, DistrictId: 34, LocalBody: 'BALEFI RURAL MUNICIPALITY' },

        { ID: 392, DistrictId: 34, LocalBody: 'SUNKOSHI RURAL MUNICIPALITY' },
        { ID: 393, DistrictId: 34, LocalBody: 'HELAMBU RURAL MUNICIPALITY' },
        { ID: 394, DistrictId: 34, LocalBody: 'BHOTEKOSHI RURAL MUNICIPALITY' },
        { ID: 395, DistrictId: 34, LocalBody: 'LISANGKHU PAKHAR RURAL MUNICIPALITY' },
        { ID: 396, DistrictId: 34, LocalBody: 'INDRAWATI RURAL MUNICIPALITY' },
        { ID: 397, DistrictId: 34, LocalBody: 'TRIPURASUNDARI RURAL MUNICIPALITY' },
        { ID: 398, DistrictId: 34, LocalBody: 'PANCHPOKHARI THANGPAL RURAL MUNICIPALITY' },
        { ID: 399, DistrictId: 34, LocalBody: 'CHAUTARA SANGACHOKGADHI MUNICIPALITY' },
        { ID: 400, DistrictId: 34, LocalBody: 'BARHABISE MUNICIPALITY' },
        { ID: 401, DistrictId: 34, LocalBody: 'MELAMCHI MUNICIPALITY' },
        { ID: 402, DistrictId: 33, LocalBody: 'KALIKA RURAL MUNICIPALITY' },
        { ID: 403, DistrictId: 33, LocalBody: 'NAUKUNDA RURAL MUNICIPALITY' },
        { ID: 404, DistrictId: 33, LocalBody: 'UTTARGAYA RURAL MUNICIPALITY' },

        { ID: 405, DistrictId: 33, LocalBody: 'GOSAIKUNDA RURAL MUNICIPALITY' },
        { ID: 406, DistrictId: 33, LocalBody: 'AMACHODINGMO RURAL MUNICIPALITY' },
        { ID: 407, DistrictId: 25, LocalBody: 'GAJURI RURAL MUNICIPALITY' },
        { ID: 408, DistrictId: 25, LocalBody: 'GALCHI RURAL MUNICIPALITY' },
        { ID: 409, DistrictId: 25, LocalBody: 'THAKRE RURAL MUNICIPALITY' },
        { ID: 410, DistrictId: 25, LocalBody: 'SIDDHALEK RURAL MUNICIPALITY' },
        { ID: 411, DistrictId: 25, LocalBody: 'KHANIYABASH RURAL MUNICIPALITY' },
        { ID: 412, DistrictId: 25, LocalBody: 'JWALAMUKHI RURAL MUNICIPALITY' },
        { ID: 413, DistrictId: 25, LocalBody: 'GANGAJAMUNA RURAL MUNICIPALITY' },
        { ID: 414, DistrictId: 25, LocalBody: 'RUBI VALLEY RURAL MUNICIPALITY' },
        { ID: 415, DistrictId: 25, LocalBody: 'TRIPURA SUNDARI RURAL MUNICIPALITY' },
        { ID: 416, DistrictId: 25, LocalBody: 'NETRAWATI DABJONG RURAL MUNICIPALITY' },
        { ID: 417, DistrictId: 25, LocalBody: 'BENIGHAT RORANG RURAL MUNICIPALITY' },
        { ID: 418, DistrictId: 25, LocalBody: 'NILAKANTHA MUNICIPALITY' },
        { ID: 419, DistrictId: 25, LocalBody: 'DHUNIBESI MUNICIPALITY' },
        { ID: 420, DistrictId: 31, LocalBody: 'KAKANI RURAL MUNICIPALITY' },
        { ID: 421, DistrictId: 31, LocalBody: 'TADI RURAL MUNICIPALITY' },
        { ID: 422, DistrictId: 31, LocalBody: 'LIKHU RURAL MUNICIPALITY' },
        { ID: 423, DistrictId: 31, LocalBody: 'MYAGANG RURAL MUNICIPALITY' },
        { ID: 424, DistrictId: 31, LocalBody: 'SHIVAPURI RURAL MUNICIPALITY' },
        { ID: 425, DistrictId: 31, LocalBody: 'KISPANG RURAL MUNICIPALITY' },
        { ID: 426, DistrictId: 31, LocalBody: 'SURYAGADHI RURAL MUNICIPALITY' },
        { ID: 427, DistrictId: 31, LocalBody: 'TARKESHWAR RURAL MUNICIPALITY' },
        { ID: 428, DistrictId: 31, LocalBody: 'PANCHAKANYA RURAL MUNICIPALITY' },
        { ID: 429, DistrictId: 31, LocalBody: 'DUPCHESHWAR RURAL MUNICIPALITY' },
        { ID: 430, DistrictId: 31, LocalBody: 'BELKOTGADHI MUNICIPALITY' },
        { ID: 431, DistrictId: 31, LocalBody: 'BIDUR MUNICIPALITY' },
        { ID: 432, DistrictId: 27, LocalBody: 'KIRTIPUR MUNICIPALITY' },
        { ID: 433, DistrictId: 27, LocalBody: 'SHANKHARAPUR MUNICIPALITY' },
        { ID: 434, DistrictId: 27, LocalBody: 'NAGARJUN MUNICIPALITY' },
        { ID: 435, DistrictId: 27, LocalBody: 'KAGESHWORI MANAHORA MUNICIPALITY' },
        { ID: 436, DistrictId: 27, LocalBody: 'DAKSHINKALI MUNICIPALITY' },
        { ID: 437, DistrictId: 27, LocalBody: 'BUDHANILAKANTHA MUNICIPALITY' },
        { ID: 438, DistrictId: 27, LocalBody: 'TARAKESHWOR MUNICIPALITY' },
        { ID: 439, DistrictId: 27, LocalBody: 'KATHMANDU METROPOLITIAN CITY' },
        { ID: 440, DistrictId: 27, LocalBody: 'TOKHA MUNICIPALITY' },
        { ID: 441, DistrictId: 27, LocalBody: 'CHANDRAGIRI MUNICIPALITY' },
        { ID: 442, DistrictId: 27, LocalBody: 'GOKARNESHWOR MUNICIPALITY' },
        { ID: 443, DistrictId: 23, LocalBody: 'CHANGUNARAYAN MUNICIPALITY' },
        { ID: 444, DistrictId: 23, LocalBody: 'SURYABINAYAK MUNICIPALITY' },
        { ID: 445, DistrictId: 23, LocalBody: 'BHAKTAPUR MUNICIPALITY' },
        { ID: 446, DistrictId: 23, LocalBody: 'MADHYAPUR THIMI MUNICIPALITY' },
        { ID: 447, DistrictId: 29, LocalBody: 'BAGMATI RURAL MUNICIPALITY' },
        { ID: 448, DistrictId: 29, LocalBody: 'MAHANKAL RURAL MUNICIPALITY' },
        { ID: 449, DistrictId: 29, LocalBody: 'KONJYOSOM RURAL MUNICIPALITY' },
        { ID: 450, DistrictId: 29, LocalBody: 'LALITPUR METROPOLITIAN CITY' },
        { ID: 451, DistrictId: 29, LocalBody: 'MAHALAXMI MUNICIPALITY' },
        { ID: 452, DistrictId: 29, LocalBody: 'GODAWARI MUNICIPALITY' },

        { ID: 453, DistrictId: 28, LocalBody: 'ROSHI RURAL MUNICIPALITY' },
        { ID: 454, DistrictId: 28, LocalBody: 'TEMAL RURAL MUNICIPALITY' },
        { ID: 455, DistrictId: 28, LocalBody: 'BHUMLU RURAL MUNICIPALITY' },
        { ID: 456, DistrictId: 28, LocalBody: 'MAHABHARAT RURAL MUNICIPALITY' },
        { ID: 457, DistrictId: 28, LocalBody: 'BETHANCHOWK RURAL MUNICIPALITY' },
        { ID: 458, DistrictId: 28, LocalBody: 'KHANIKHOLA RURAL MUNICIPALITY' },
        { ID: 459, DistrictId: 28, LocalBody: 'CHAURIDEURALI RURAL MUNICIPALITY' },
        { ID: 460, DistrictId: 28, LocalBody: 'BANEPA MUNICIPALITY' },
        { ID: 461, DistrictId: 28, LocalBody: 'MANDANDEUPUR MUNICIPALITY' },
        { ID: 462, DistrictId: 28, LocalBody: 'DHULIKHEL MUNICIPALITY' },
        { ID: 463, DistrictId: 28, LocalBody: 'PANAUTI MUNICIPALITY' },
        { ID: 464, DistrictId: 28, LocalBody: 'NAMOBUDDHA MUNICIPALITY' },
        { ID: 465, DistrictId: 28, LocalBody: 'PANCHKHAL MUNICIPALITY' },
        { ID: 466, DistrictId: 32, LocalBody: 'SUNAPATI RURAL MUNICIPALITY' },
        { ID: 467, DistrictId: 32, LocalBody: 'DORAMBA RURAL MUNICIPALITY' },
        { ID: 468, DistrictId: 32, LocalBody: 'UMAKUNDA RURAL MUNICIPALITY' },
        { ID: 469, DistrictId: 32, LocalBody: 'KHADADEVI RURAL MUNICIPALITY' },
        { ID: 470, DistrictId: 32, LocalBody: 'GOKULGANGA RURAL MUNICIPALITY' },
        { ID: 471, DistrictId: 32, LocalBody: 'LIKHU TAMAKOSHI RURAL MUNICIPALITY' },
        { ID: 472, DistrictId: 32, LocalBody: 'MANTHALI MUNICIPALITY' },
        { ID: 473, DistrictId: 32, LocalBody: 'RAMECHHAP MUNICIPALITY' },
        { ID: 474, DistrictId: 35, LocalBody: 'MARIN RURAL MUNICIPALITY' },
        { ID: 475, DistrictId: 35, LocalBody: 'PHIKKAL RURAL MUNICIPALITY' },
        { ID: 476, DistrictId: 35, LocalBody: 'TINPATAN RURAL MUNICIPALITY' },
        { ID: 477, DistrictId: 35, LocalBody: 'SUNKOSHI RURAL MUNICIPALITY' },
        { ID: 478, DistrictId: 35, LocalBody: 'GOLANJOR RURAL MUNICIPALITY' },
        { ID: 479, DistrictId: 35, LocalBody: 'GHANGLEKH RURAL MUNICIPALITY' },
        { ID: 480, DistrictId: 35, LocalBody: 'HARIHARPURGADHI RURAL MUNICIPALITY' },
        { ID: 481, DistrictId: 35, LocalBody: 'DUDHOULI MUNICIPALITY' },
        { ID: 482, DistrictId: 35, LocalBody: 'KAMALAMAI MUNICIPALITY' },
        { ID: 483, DistrictId: 30, LocalBody: 'BAKAIYA RURAL MUNICIPALITY' },
        { ID: 484, DistrictId: 30, LocalBody: 'KAILASH RURAL MUNICIPALITY' },
        { ID: 485, DistrictId: 30, LocalBody: 'MANAHARI RURAL MUNICIPALITY' },
        { ID: 486, DistrictId: 30, LocalBody: 'BHIMPHEDI RURAL MUNICIPALITY' },
        { ID: 487, DistrictId: 30, LocalBody: 'BAGMATI RURAL MUNICIPALITY' },
        { ID: 488, DistrictId: 30, LocalBody: 'RAKSIRANG RURAL MUNICIPALITY' },
        { ID: 489, DistrictId: 30, LocalBody: 'MAKAWANPURGADHI RURAL MUNICIPALITY' },
        { ID: 490, DistrictId: 30, LocalBody: 'INDRASAROWAR RURAL MUNICIPALITY' },
        { ID: 491, DistrictId: 30, LocalBody: 'HETAUDA SUB-METROPOLITIAN CITY' },
        { ID: 492, DistrictId: 30, LocalBody: 'THAHA MUNICIPALITY' },

        { ID: 493, DistrictId: 24, LocalBody: 'ICHCHHYAKAMANA RURAL MUNICIPALITY' },
        { ID: 494, DistrictId: 24, LocalBody: 'BHARATPUR METROPOLITIAN CITY' },
        { ID: 495, DistrictId: 24, LocalBody: 'KALIKA MUNICIPALITY' },
        { ID: 496, DistrictId: 24, LocalBody: 'KHAIRAHANI MUNICIPALITY' },
        { ID: 497, DistrictId: 24, LocalBody: 'MADI MUNICIPALITY' },
        { ID: 498, DistrictId: 24, LocalBody: 'RAPTI MUNICIPALITY' },
        { ID: 499, DistrictId: 24, LocalBody: 'RATNANAGAR MUNICIPALITY' },
        { ID: 500, DistrictId: 37, LocalBody: 'GANDAKI RURAL MUNICIPALITY' },
        { ID: 501, DistrictId: 37, LocalBody: 'DHARCHE RURAL MUNICIPALITY' },
        { ID: 502, DistrictId: 37, LocalBody: 'AARUGHAT RURAL MUNICIPALITY' },
        { ID: 503, DistrictId: 37, LocalBody: 'AJIRKOT RURAL MUNICIPALITY' },
        { ID: 504, DistrictId: 37, LocalBody: 'SAHID LAKHAN RURAL MUNICIPALITY' },

        { ID: 505, DistrictId: 37, LocalBody: 'SIRANCHOK RURAL MUNICIPALITY' },
        { ID: 506, DistrictId: 37, LocalBody: 'BHIMSENTHAPA RURAL MUNICIPALITY' },
        { ID: 507, DistrictId: 37, LocalBody: 'CHUM NUBRI RURAL MUNICIPALITY' },
        { ID: 508, DistrictId: 37, LocalBody: 'BARPAK SULIKOT RURAL MUNICIPALITY' },
        { ID: 509, DistrictId: 37, LocalBody: 'PALUNGTAR MUNICIPALITY' },
        { ID: 510, DistrictId: 37, LocalBody: 'GORKHA MUNICIPALITY' },
        { ID: 511, DistrictId: 40, LocalBody: 'CHAME RURAL MUNICIPALITY' },
        { ID: 512, DistrictId: 40, LocalBody: 'NARSHON RURAL MUNICIPALITY' },
        { ID: 513, DistrictId: 40, LocalBody: 'NARPA BHUMI RURAL MUNICIPALITY' },
        { ID: 514, DistrictId: 40, LocalBody: 'MANANG INGSHYANG RURAL MUNICIPALITY' },
        { ID: 515, DistrictId: 41, LocalBody: 'THASANG RURAL MUNICIPALITY' },
        { ID: 516, DistrictId: 41, LocalBody: 'GHARAPJHONG RURAL MUNICIPALITY' },
        { ID: 517, DistrictId: 41, LocalBody: 'LOMANTHANG RURAL MUNICIPALITY' },
        { ID: 518, DistrictId: 41, LocalBody: 'LO-GHEKAR DAMODARKUNDA RURAL MUNICIPALITY' },
        { ID: 519, DistrictId: 41, LocalBody: 'WARAGUNG MUKTIKHSETRA RURAL MUNICIPALITY' },
        { ID: 520, DistrictId: 42, LocalBody: 'MANGALA RURAL MUNICIPALITY' },
        { ID: 521, DistrictId: 42, LocalBody: 'MALIKA RURAL MUNICIPALITY' },
        { ID: 522, DistrictId: 42, LocalBody: 'RAGHUGANGA RURAL MUNICIPALITY' },
        { ID: 523, DistrictId: 42, LocalBody: 'DHAULAGIRI RURAL MUNICIPALITY' },
        { ID: 524, DistrictId: 42, LocalBody: 'ANNAPURNA RURAL MUNICIPALITY' },
        { ID: 525, DistrictId: 42, LocalBody: 'BENI MUNICIPALITY' },
        { ID: 526, DistrictId: 38, LocalBody: 'RUPA RURAL MUNICIPALITY' },
        { ID: 527, DistrictId: 38, LocalBody: 'MADI RURAL MUNICIPALITY' },
        { ID: 528, DistrictId: 38, LocalBody: 'ANNAPURNA RURAL MUNICIPALITY' },
        { ID: 529, DistrictId: 38, LocalBody: 'MACHHAPUCHCHHRE RURAL MUNICIPALITY' },
        { ID: 530, DistrictId: 38, LocalBody: 'POKHARA METROPOLITIAN CITY' },
        { ID: 531, DistrictId: 39, LocalBody: 'DORDI RURAL MUNICIPALITY' },
        { ID: 532, DistrictId: 39, LocalBody: 'DUDHPOKHARI RURAL MUNICIPALITY' },
        { ID: 533, DistrictId: 39, LocalBody: 'MARSYANGDI RURAL MUNICIPALITY' },
        { ID: 534, DistrictId: 39, LocalBody: 'KWHOLASOTHAR RURAL MUNICIPALITY' },
        { ID: 535, DistrictId: 39, LocalBody: 'SUNDARBAZAR MUNICIPALITY' },
        { ID: 536, DistrictId: 39, LocalBody: 'BESISHAHAR MUNICIPALITY' },
        { ID: 537, DistrictId: 39, LocalBody: 'RAINAS MUNICIPALITY' },
        { ID: 538, DistrictId: 39, LocalBody: 'MADHYANEPAL MUNICIPALITY' },
        { ID: 539, DistrictId: 46, LocalBody: 'GHIRING RURAL MUNICIPALITY' },
        { ID: 540, DistrictId: 46, LocalBody: 'DEVGHAT RURAL MUNICIPALITY' },
        { ID: 541, DistrictId: 46, LocalBody: 'RHISHING RURAL MUNICIPALITY' },
        { ID: 542, DistrictId: 46, LocalBody: 'MYAGDE RURAL MUNICIPALITY' },
        { ID: 543, DistrictId: 46, LocalBody: 'BANDIPUR RURAL MUNICIPALITY' },
        { ID: 544, DistrictId: 46, LocalBody: 'ANBUKHAIRENI RURAL MUNICIPALITY' },
        { ID: 545, DistrictId: 46, LocalBody: 'BYAS MUNICIPALITY' },
        { ID: 546, DistrictId: 46, LocalBody: 'SHUKLAGANDAKI MUNICIPALITY' },
        { ID: 547, DistrictId: 46, LocalBody: 'BHIMAD MUNICIPALITY' },
        { ID: 548, DistrictId: 46, LocalBody: 'BHANU MUNICIPALITY' },
        { ID: 549, DistrictId: 47, LocalBody: 'BAUDEEKALI RURAL MUNICIPALITY' },
        { ID: 550, DistrictId: 47, LocalBody: 'BULINGTAR RURAL MUNICIPALITY' },
        { ID: 551, DistrictId: 47, LocalBody: 'HUPSEKOT RURAL MUNICIPALITY' },
        { ID: 552, DistrictId: 47, LocalBody: 'BINAYEE TRIBENI RURAL MUNICIPALITY' },
        { ID: 553, DistrictId: 47, LocalBody: 'MADHYABINDU MUNICIPALITY' },
        { ID: 554, DistrictId: 47, LocalBody: 'DEVCHULI MUNICIPALITY' },
        { ID: 555, DistrictId: 47, LocalBody: 'GAIDAKOT MUNICIPALITY' },
        { ID: 556, DistrictId: 47, LocalBody: 'KAWASOTI MUNICIPALITY' },
        { ID: 557, DistrictId: 45, LocalBody: 'HARINAS RURAL MUNICIPALITY' },

        { ID: 558, DistrictId: 45, LocalBody: 'BIRUWA RURAL MUNICIPALITY' },
        { ID: 559, DistrictId: 45, LocalBody: 'AANDHIKHOLA RURAL MUNICIPALITY' },
        { ID: 560, DistrictId: 45, LocalBody: 'PHEDIKHOLA RURAL MUNICIPALITY' },
        { ID: 561, DistrictId: 45, LocalBody: 'KALIGANDAGI RURAL MUNICIPALITY' },
        { ID: 562, DistrictId: 45, LocalBody: 'ARJUNCHAUPARI RURAL MUNICIPALITY' },
        { ID: 563, DistrictId: 45, LocalBody: 'PUTALIBAZAR MUNICIPALITY' },
        { ID: 564, DistrictId: 45, LocalBody: 'BHIRKOT MUNICIPALITY' },
        { ID: 565, DistrictId: 45, LocalBody: 'GALYANG MUNICIPALITY' },
        { ID: 566, DistrictId: 45, LocalBody: 'CHAPAKOT MUNICIPALITY' },
        { ID: 567, DistrictId: 45, LocalBody: 'WALING MUNICIPALITY' },
        { ID: 568, DistrictId: 44, LocalBody: 'MODI RURAL MUNICIPALITY' },
        { ID: 569, DistrictId: 44, LocalBody: 'PAINYU RURAL MUNICIPALITY' },
        { ID: 570, DistrictId: 44, LocalBody: 'JALJALA RURAL MUNICIPALITY' },
        { ID: 571, DistrictId: 44, LocalBody: 'BIHADI RURAL MUNICIPALITY' },
        { ID: 572, DistrictId: 44, LocalBody: 'MAHASHILA RURAL MUNICIPALITY' },
        { ID: 573, DistrictId: 44, LocalBody: 'KUSHMA MUNICIPALITY' },
        { ID: 574, DistrictId: 44, LocalBody: 'PHALEBAS MUNICIPALITY' },
        { ID: 575, DistrictId: 36, LocalBody: 'BARENG RURAL MUNICIPALITY' },
        { ID: 576, DistrictId: 36, LocalBody: 'BADIGAD RURAL MUNICIPALITY' },
        { ID: 577, DistrictId: 36, LocalBody: 'NISIKHOLA RURAL MUNICIPALITY' },
        { ID: 578, DistrictId: 36, LocalBody: 'KANTHEKHOLA RURAL MUNICIPALITY' },
        { ID: 579, DistrictId: 36, LocalBody: 'TARA KHOLA RURAL MUNICIPALITY' },
        { ID: 580, DistrictId: 36, LocalBody: 'TAMAN KHOLA RURAL MUNICIPALITY' },
        { ID: 581, DistrictId: 36, LocalBody: 'JAIMUNI MUNICIPALITY' },
        { ID: 582, DistrictId: 36, LocalBody: 'BAGLUNG MUNICIPALITY' },
        { ID: 583, DistrictId: 36, LocalBody: 'GALKOT MUNICIPALITY' },
        { ID: 584, DistrictId: 36, LocalBody: 'DHORPATAN MUNICIPALITY' },
        { ID: 585, DistrictId: 63, LocalBody: 'KAIKE RURAL MUNICIPALITY' },
        { ID: 586, DistrictId: 63, LocalBody: 'JAGADULLA RURAL MUNICIPALITY' },
        { ID: 587, DistrictId: 63, LocalBody: 'MUDKECHULA RURAL MUNICIPALITY' },
        { ID: 588, DistrictId: 63, LocalBody: 'DOLPO BUDDHA RURAL MUNICIPALITY' },
        { ID: 589, DistrictId: 63, LocalBody: 'SHEY PHOKSUNDO RURAL MUNICIPALITY' },
        { ID: 590, DistrictId: 63, LocalBody: 'CHHARKA TANGSONG RURAL MUNICIPALITY' },
        { ID: 591, DistrictId: 63, LocalBody: 'TRIPURASUNDARI MUNICIPALITY' },
        { ID: 592, DistrictId: 63, LocalBody: 'THULI BHERI MUNICIPALITY' },
        { ID: 593, DistrictId: 68, LocalBody: 'SORU RURAL MUNICIPALITY' },
        { ID: 594, DistrictId: 68, LocalBody: 'KHATYAD RURAL MUNICIPALITY' },
        { ID: 595, DistrictId: 68, LocalBody: 'MUGUM KARMARONG RURAL MUNICIPALITY' },
        { ID: 596, DistrictId: 68, LocalBody: 'CHHAYANATH RARA MUNICIPALITY' },
        { ID: 597, DistrictId: 64, LocalBody: 'SIMKOT RURAL MUNICIPALITY' },
        { ID: 598, DistrictId: 64, LocalBody: 'NAMKHA RURAL MUNICIPALITY' },
        { ID: 599, DistrictId: 64, LocalBody: 'CHANKHELI RURAL MUNICIPALITY' },
        { ID: 600, DistrictId: 64, LocalBody: 'TANJAKOT RURAL MUNICIPALITY' },
        { ID: 601, DistrictId: 64, LocalBody: 'SARKEGAD RURAL MUNICIPALITY' },
        { ID: 602, DistrictId: 64, LocalBody: 'ADANCHULI RURAL MUNICIPALITY' },
        { ID: 603, DistrictId: 64, LocalBody: 'KHARPUNATH RURAL MUNICIPALITY' },
        { ID: 604, DistrictId: 66, LocalBody: 'HIMA RURAL MUNICIPALITY' },
        { ID: 605, DistrictId: 66, LocalBody: 'TILA RURAL MUNICIPALITY' },
        { ID: 606, DistrictId: 66, LocalBody: 'SINJA RURAL MUNICIPALITY' },
        { ID: 607, DistrictId: 66, LocalBody: 'GUTHICHAUR RURAL MUNICIPALITY' },
        { ID: 608, DistrictId: 66, LocalBody: 'TATOPANI RURAL MUNICIPALITY' },
        { ID: 609, DistrictId: 66, LocalBody: 'PATRASI RURAL MUNICIPALITY' },
        { ID: 610, DistrictId: 66, LocalBody: 'KANAKASUNDARI RURAL MUNICIPALITY' },
        { ID: 611, DistrictId: 66, LocalBody: 'CHANDANNATH MUNICIPALITY' },
        { ID: 612, DistrictId: 67, LocalBody: 'MAHAWAI RURAL MUNICIPALITY' },
        { ID: 613, DistrictId: 67, LocalBody: 'PALATA RURAL MUNICIPALITY' },
        { ID: 614, DistrictId: 67, LocalBody: 'NARAHARINATH RURAL MUNICIPALITY' },
        { ID: 615, DistrictId: 67, LocalBody: 'PACHALJHARANA RURAL MUNICIPALITY' },
        { ID: 616, DistrictId: 67, LocalBody: 'SUBHA KALIKA RURAL MUNICIPALITY' },
        { ID: 617, DistrictId: 67, LocalBody: 'SANNI TRIBENI RURAL MUNICIPALITY' },
        { ID: 618, DistrictId: 67, LocalBody: 'KHANDACHAKRA MUNICIPALITY' },
        { ID: 619, DistrictId: 67, LocalBody: 'RASKOT MUNICIPALITY' },
        { ID: 620, DistrictId: 67, LocalBody: 'TILAGUFA MUNICIPALITY' },
        { ID: 621, DistrictId: 62, LocalBody: 'BHAIRABI RURAL MUNICIPALITY' },
        { ID: 622, DistrictId: 62, LocalBody: 'MAHABU RURAL MUNICIPALITY' },
        { ID: 623, DistrictId: 62, LocalBody: 'GURANS RURAL MUNICIPALITY' },
        { ID: 624, DistrictId: 62, LocalBody: 'NAUMULE RURAL MUNICIPALITY' },
        { ID: 625, DistrictId: 62, LocalBody: 'BHAGAWATIMAI RURAL MUNICIPALITY' },

        { ID: 626, DistrictId: 62, LocalBody: 'THANTIKANDH RURAL MUNICIPALITY' },
        { ID: 627, DistrictId: 62, LocalBody: 'DUNGESHWOR RURAL MUNICIPALITY' },
        { ID: 628, DistrictId: 62, LocalBody: 'AATHABIS MUNICIPALITY' },
        { ID: 629, DistrictId: 62, LocalBody: 'DULLU MUNICIPALITY' },
        { ID: 630, DistrictId: 62, LocalBody: 'CHAMUNDA BINDRASAINI MUNICIPALITY' },
        { ID: 631, DistrictId: 62, LocalBody: 'NARAYAN MUNICIPALITY' },
        { ID: 632, DistrictId: 65, LocalBody: 'KUSE RURAL MUNICIPALITY' },
        { ID: 633, DistrictId: 65, LocalBody: 'SHIWALAYA RURAL MUNICIPALITY' },
        { ID: 634, DistrictId: 65, LocalBody: 'BAREKOT RURAL MUNICIPALITY' },
        { ID: 635, DistrictId: 65, LocalBody: 'JUNICHANDE RURAL MUNICIPALITY' },
        { ID: 636, DistrictId: 65, LocalBody: 'NALAGAD MUNICIPALITY' },
        { ID: 637, DistrictId: 65, LocalBody: 'BHERI MUNICIPALITY' },
        { ID: 638, DistrictId: 65, LocalBody: 'CHHEDAGAD MUNICIPALITY' },
        { ID: 639, DistrictId: 61, LocalBody: 'TRIBENI RURAL MUNICIPALITY' },
        { ID: 640, DistrictId: 61, LocalBody: 'SANI BHERI RURAL MUNICIPALITY' },
        { ID: 641, DistrictId: 61, LocalBody: 'BANFIKOT RURAL MUNICIPALITY' },
        { ID: 642, DistrictId: 61, LocalBody: 'AATHBISKOT MUNICIPALITY' },
        { ID: 643, DistrictId: 61, LocalBody: 'CHAURJAHARI MUNICIPALITY' },
        { ID: 644, DistrictId: 61, LocalBody: 'MUSIKOT MUNICIPALITY' },
        { ID: 645, DistrictId: 69, LocalBody: 'KUMAKH RURAL MUNICIPALITY' },
        { ID: 646, DistrictId: 69, LocalBody: 'DARMA RURAL MUNICIPALITY' },
        { ID: 647, DistrictId: 69, LocalBody: 'KAPURKOT RURAL MUNICIPALITY' },
        { ID: 648, DistrictId: 69, LocalBody: 'KALIMATI RURAL MUNICIPALITY' },
        { ID: 649, DistrictId: 69, LocalBody: 'TRIBENI RURAL MUNICIPALITY' },
        { ID: 650, DistrictId: 69, LocalBody: 'CHHATRESHWORI RURAL MUNICIPALITY' },
        { ID: 651, DistrictId: 69, LocalBody: 'SIDDHA KUMAKH RURAL MUNICIPALITY' },
        { ID: 652, DistrictId: 69, LocalBody: 'SHARADA MUNICIPALITY' },
        { ID: 653, DistrictId: 69, LocalBody: 'BANGAD KUPINDE MUNICIPALITY' },
        { ID: 654, DistrictId: 69, LocalBody: 'BANGAD KUPINDE MUNICIPALITY' },
        { ID: 655, DistrictId: 70, LocalBody: 'CHAUKUNE RURAL MUNICIPALITY' },
        { ID: 656, DistrictId: 70, LocalBody: 'SIMTA RURAL MUNICIPALITY' },
        { ID: 657, DistrictId: 70, LocalBody: 'CHINGAD RURAL MUNICIPALITY' },
        { ID: 658, DistrictId: 70, LocalBody: 'BARAHTAL RURAL MUNICIPALITY' },
        { ID: 659, DistrictId: 70, LocalBody: 'GURBHAKOT MUNICIPALITY' },
        { ID: 660, DistrictId: 70, LocalBody: 'PANCHPURI MUNICIPALITY' },
        { ID: 661, DistrictId: 70, LocalBody: 'BHERIGANGA MUNICIPALITY' },
        { ID: 662, DistrictId: 70, LocalBody: 'LEKBESHI MUNICIPALITY' },
        { ID: 663, DistrictId: 70, LocalBody: 'BIRENDRANAGAR MUNICIPALITY' },
        { ID: 664, DistrictId: 74, LocalBody: 'GAUMUL RURAL MUNICIPALITY' },
        { ID: 665, DistrictId: 74, LocalBody: 'HIMALI RURAL MUNICIPALITY' },
        { ID: 666, DistrictId: 74, LocalBody: 'JAGANNATH RURAL MUNICIPALITY' },
        { ID: 667, DistrictId: 74, LocalBody: 'KHAPTAD CHHEDEDAHA RURAL MUNICIPALITY' },
        { ID: 668, DistrictId: 74, LocalBody: 'SWAMI KARTIK KHAAPAR RURAL MUNICIPALITY' },
        { ID: 669, DistrictId: 74, LocalBody: 'BADIMALIKA MUNICIPALITY' },
        { ID: 670, DistrictId: 74, LocalBody: 'TRIBENI MUNICIPALITY' },
        { ID: 671, DistrictId: 74, LocalBody: 'BUDHIGANGA MUNICIPALITY' },
        { ID: 672, DistrictId: 74, LocalBody: 'BUDHINANDA MUNICIPALITY' },
        { ID: 673, DistrictId: 73, LocalBody: 'MASTA RURAL MUNICIPALITY' },
        { ID: 674, DistrictId: 73, LocalBody: 'THALARA RURAL MUNICIPALITY' },
        { ID: 675, DistrictId: 73, LocalBody: 'TALKOT RURAL MUNICIPALITY' },
        { ID: 676, DistrictId: 73, LocalBody: 'SURMA RURAL MUNICIPALITY' },
        { ID: 677, DistrictId: 73, LocalBody: 'SAIPAAL RURAL MUNICIPALITY' },
        { ID: 678, DistrictId: 73, LocalBody: 'DURGATHALI RURAL MUNICIPALITY' },
        { ID: 679, DistrictId: 73, LocalBody: 'BITHADCHIR RURAL MUNICIPALITY' },
        { ID: 680, DistrictId: 73, LocalBody: 'KEDARSEU RURAL MUNICIPALITY' },
        { ID: 681, DistrictId: 73, LocalBody: 'KHAPTADCHHANNA RURAL MUNICIPALITY' },
        { ID: 682, DistrictId: 73, LocalBody: 'CHABISPATHIVERA RURAL MUNICIPALITY' },
        { ID: 683, DistrictId: 73, LocalBody: 'JAYAPRITHIVI MUNICIPALITY' },
        { ID: 684, DistrictId: 73, LocalBody: 'BUNGAL MUNICIPALITY' },
        { ID: 685, DistrictId: 76, LocalBody: 'LEKAM RURAL MUNICIPALITY' },
        { ID: 686, DistrictId: 76, LocalBody: 'NAUGAD RURAL MUNICIPALITY' },
        { ID: 687, DistrictId: 76, LocalBody: 'BYAS RURAL MUNICIPALITY' },
        { ID: 688, DistrictId: 76, LocalBody: 'DUNHU RURAL MUNICIPALITY' },
        { ID: 689, DistrictId: 76, LocalBody: 'MARMA RURAL MUNICIPALITY' },
        { ID: 690, DistrictId: 76, LocalBody: 'APIHIMAL RURAL MUNICIPALITY' },
        { ID: 691, DistrictId: 76, LocalBody: 'MALIKAARJUN RURAL MUNICIPALITY' },
        { ID: 692, DistrictId: 76, LocalBody: 'MAHAKALI MUNICIPALITY' },
        { ID: 693, DistrictId: 76, LocalBody: 'SHAILYASHIKHAR MUNICIPALITY' },
        { ID: 694, DistrictId: 72, LocalBody: 'SIGAS RURAL MUNICIPALITY' },
        { ID: 695, DistrictId: 72, LocalBody: 'SHIVANATH RURAL MUNICIPALITY' },
        { ID: 696, DistrictId: 72, LocalBody: 'SURNAYA RURAL MUNICIPALITY' },
        { ID: 697, DistrictId: 72, LocalBody: 'DILASAINI RURAL MUNICIPALITY' },
        { ID: 698, DistrictId: 72, LocalBody: 'PANCHESHWAR RURAL MUNICIPALITY' },
        { ID: 699, DistrictId: 72, LocalBody: 'DOGADAKEDAR RURAL MUNICIPALITY' },
        { ID: 700, DistrictId: 72, LocalBody: 'MELAULI MUNICIPALITY' },

        { ID: 701, DistrictId: 72, LocalBody: 'DASHARATHCHANDA MUNICIPALITY' },
        { ID: 702, DistrictId: 72, LocalBody: 'PURCHAUDI MUNICIPALITY' },
        { ID: 703, DistrictId: 72, LocalBody: 'PATAN MUNICIPALITY' },
        { ID: 704, DistrictId: 75, LocalBody: 'ALITAL RURAL MUNICIPALITY' },
        { ID: 705, DistrictId: 75, LocalBody: 'AJAYMERU RURAL MUNICIPALITY' },
        { ID: 706, DistrictId: 75, LocalBody: 'BHAGESHWAR RURAL MUNICIPALITY' },
        { ID: 707, DistrictId: 75, LocalBody: 'NAWADURGA RURAL MUNICIPALITY' },
        { ID: 708, DistrictId: 75, LocalBody: 'GANAYAPDHURA RURAL MUNICIPALITY' },
        { ID: 709, DistrictId: 75, LocalBody: 'AMARGADHI MUNICIPALITY' },
        { ID: 710, DistrictId: 75, LocalBody: 'PARASHURAM MUNICIPALITY' },
        { ID: 711, DistrictId: 77, LocalBody: 'SAYAL RURAL MUNICIPALITY' },
        { ID: 712, DistrictId: 77, LocalBody: 'ADHARSHA RURAL MUNICIPALITY' },
        { ID: 713, DistrictId: 77, LocalBody: 'JORAYAL RURAL MUNICIPALITY' },
        { ID: 714, DistrictId: 77, LocalBody: 'BADIKEDAR RURAL MUNICIPALITY' },
        { ID: 715, DistrictId: 77, LocalBody: 'PURBICHAUKI RURAL MUNICIPALITY' },
        { ID: 716, DistrictId: 77, LocalBody: 'K I SINGH RURAL MUNICIPALITY' },
        { ID: 717, DistrictId: 77, LocalBody: 'BOGTAN FOODSIL RURAL MUNICIPALITY' },
        { ID: 718, DistrictId: 77, LocalBody: 'DIPAYAL SILGADI MUNICIPALITY' },
        { ID: 719, DistrictId: 77, LocalBody: 'SHIKHAR MUNICIPALITY' },

        { ID: 720, DistrictId: 71, LocalBody: 'DHAKARI RURAL MUNICIPALITY' },
        { ID: 721, DistrictId: 71, LocalBody: 'MELLEKH RURAL MUNICIPALITY' },
        { ID: 722, DistrictId: 71, LocalBody: 'CHAURPATI RURAL MUNICIPALITY' },
        { ID: 723, DistrictId: 71, LocalBody: 'RAMAROSHAN RURAL MUNICIPALITY' },
        { ID: 724, DistrictId: 71, LocalBody: 'TURMAKHAD RURAL MUNICIPALITY' },
        { ID: 725, DistrictId: 71, LocalBody: 'BANNIGADHI JAYAGADH RURAL MUNICIPALITY' },
        { ID: 726, DistrictId: 71, LocalBody: 'SANPHEBAGAR MUNICIPALITY' },
        { ID: 727, DistrictId: 71, LocalBody: 'MANGALSEN MUNICIPALITY' },
        { ID: 728, DistrictId: 71, LocalBody: 'KAMALBAZAR MUNICIPALITY' },
        { ID: 729, DistrictId: 71, LocalBody: 'PANCHADEWAL BINAYAK MUNICIPALITY' },
        { ID: 730, DistrictId: 78, LocalBody: 'CHURE RURAL MUNICIPALITY' },
        { ID: 731, DistrictId: 78, LocalBody: 'JANAKI RURAL MUNICIPALITY' },
        { ID: 732, DistrictId: 78, LocalBody: 'KAILARI RURAL MUNICIPALITY' },
        { ID: 733, DistrictId: 78, LocalBody: 'JOSHIPUR RURAL MUNICIPALITY' },
        { ID: 734, DistrictId: 78, LocalBody: 'MOHANYAL RURAL MUNICIPALITY' },
        { ID: 735, DistrictId: 78, LocalBody: 'BARDAGORIYA RURAL MUNICIPALITY' },
        { ID: 736, DistrictId: 78, LocalBody: 'TIKAPUR MUNICIPALITY' },
        { ID: 737, DistrictId: 78, LocalBody: 'GHODAGHODI MUNICIPALITY' },
        { ID: 738, DistrictId: 78, LocalBody: 'BHAJANI MUNICIPALITY' },
        { ID: 739, DistrictId: 78, LocalBody: 'DHANGADHI SUB-METROPOLITIAN CITY' },
        { ID: 740, DistrictId: 78, LocalBody: 'GAURIGANGA MUNICIPALITY' },
        { ID: 741, DistrictId: 78, LocalBody: 'GODAWARI MUNICIPALITY' },
        { ID: 742, DistrictId: 78, LocalBody: 'LAMKICHUHA MUNICIPALITY' },
        { ID: 743, DistrictId: 79, LocalBody: 'BELDANDI RURAL MUNICIPALITY' },
        { ID: 744, DistrictId: 79, LocalBody: 'LALJHADI RURAL MUNICIPALITY' },
        { ID: 745, DistrictId: 79, LocalBody: 'PUNARBAS MUNICIPALITY' },
        { ID: 746, DistrictId: 79, LocalBody: 'KRISHNAPUR MUNICIPALITY' },
        { ID: 747, DistrictId: 79, LocalBody: 'MAHAKALI MUNICIPALITY' },
        { ID: 748, DistrictId: 79, LocalBody: 'BEDKOT MUNICIPALITY' },
        { ID: 749, DistrictId: 79, LocalBody: 'BELAURI MUNICIPALITY' },
        { ID: 750, DistrictId: 79, LocalBody: 'BHIMDATTA MUNICIPALITY' },
        { ID: 751, DistrictId: 79, LocalBody: 'SHUKLAPHANTA MUNICIPALITY' },
        { ID: 752, DistrictId: 80, LocalBody: 'NA' },
        { ID: 753, DistrictId: 80, LocalBody: 'SISNE RURAL MUNICIPALITY' },
        { ID: 754, DistrictId: 80, LocalBody: 'PUTHA UTTARGANGA RURAL MUNICIPALITY' },
        ];

        //for districId
        $scope.Districtcoll = [
            { DistrictId: 1, District: 'BHOJPUR', Province: 1, PlaceOfIssue: 'BHOJP' },
            { DistrictId: 2, District: 'DHANKUTA', Province: 1, PlaceOfIssue: 'DHANK' },
            { DistrictId: 3, District: 'ILAM', Province: 1, PlaceOfIssue: 'ILAM' },
            { DistrictId: 4, District: 'JHAPA', Province: 1, PlaceOfIssue: 'JHAPA' },
            { DistrictId: 5, District: 'KHOTANG', Province: 1, PlaceOfIssue: 'KHOTA' },
            { DistrictId: 6, District: 'MORANG', Province: 1, PlaceOfIssue: 'MORAN' },
            { DistrictId: 7, District: 'OKHALDHUNGA', Province: 1, PlaceOfIssue: 'OKHAL' },
            { DistrictId: 8, District: 'PANCHTHAR', Province: 1, PlaceOfIssue: 'PANCH' },
            { DistrictId: 9, District: 'SANKHUWASABHA', Province: 1, PlaceOfIssue: 'SANKH' },
            { DistrictId: 10, District: 'SOLUKHUMBU', Province: 1, PlaceOfIssue: 'SOLUK' },
            { DistrictId: 11, District: 'SUNSARI', Province: 1, PlaceOfIssue: 'SUNSA' },
            { DistrictId: 12, District: 'TAPLEJUNG', Province: 1, PlaceOfIssue: 'TAPLE' },
            { DistrictId: 13, District: 'TERHATHUM', Province: 1, PlaceOfIssue: 'TERAT' },
            { DistrictId: 14, District: 'UDAYPUR', Province: 1, PlaceOfIssue: 'UDAYA' },
            { DistrictId: 15, District: 'BARA', Province: 2, PlaceOfIssue: 'BARA' },
            { DistrictId: 16, District: 'DHANUSHA', Province: 2, PlaceOfIssue: 'DHANU' },
            { DistrictId: 17, District: 'MAHOTTARI', Province: 2, PlaceOfIssue: 'MAHOT' },
            { DistrictId: 18, District: 'PARSA', Province: 2, PlaceOfIssue: 'PARSA' },
            { DistrictId: 19, District: 'RAUTHAHAT', Province: 2, PlaceOfIssue: 'RAUTA' },
            { DistrictId: 20, District: 'SAPTARI', Province: 2, PlaceOfIssue: 'SAPTA' },
            { DistrictId: 21, District: 'SARLAHI', Province: 2, PlaceOfIssue: 'SARLA' },
            { DistrictId: 22, District: 'SIRAHA', Province: 2, PlaceOfIssue: 'SIRAH' },
            { DistrictId: 23, District: 'BHAKTAPUR', Province: 3, PlaceOfIssue: 'BHAKT' },
            { DistrictId: 24, District: 'CHITWAN', Province: 3, PlaceOfIssue: 'CHITW' },
            { DistrictId: 25, District: 'DHADING', Province: 3, PlaceOfIssue: 'DHADI' },
            { DistrictId: 26, District: 'DOLAKHA', Province: 3, PlaceOfIssue: 'DOLAK' },
            { DistrictId: 27, District: 'KATHMANDU', Province: 3, PlaceOfIssue: 'KATHM' },
            { DistrictId: 28, District: 'KAVREPALANCHOK', Province: 3, PlaceOfIssue: 'KAVRE' },
            { DistrictId: 29, District: 'LALITPUR', Province: 3, PlaceOfIssue: 'LALIT' },
            { DistrictId: 30, District: 'MAKWANPUR', Province: 3, PlaceOfIssue: 'MAKWA' },
            { DistrictId: 31, District: 'NUWAKOT', Province: 3, PlaceOfIssue: 'NUWAK' },
            { DistrictId: 32, District: 'RAMECHHAP', Province: 3, PlaceOfIssue: 'RAMEC' },
            { DistrictId: 33, District: 'RASUWA', Province: 3, PlaceOfIssue: 'RASUW' },
            { DistrictId: 34, District: 'SINDHUPALCHOK', Province: 3, PlaceOfIssue: 'SINDU' },
            { DistrictId: 35, District: 'SINDHULI', Province: 3, PlaceOfIssue: 'SINDH' },
            { DistrictId: 36, District: 'BAGLUNG', Province: 4, PlaceOfIssue: 'BAGLU' },
            { DistrictId: 37, District: 'GORKHA', Province: 4, PlaceOfIssue: 'GORKH' },
            { DistrictId: 38, District: 'KASKI', Province: 4, PlaceOfIssue: 'KASKI' },
            { DistrictId: 39, District: 'LAMJUNG', Province: 4, PlaceOfIssue: 'LAMJU' },
            { DistrictId: 40, District: 'MANAG', Province: 4, PlaceOfIssue: 'MANAN' },
            { DistrictId: 41, District: 'MUSTANG', Province: 4, PlaceOfIssue: 'MUSTA' },
            { DistrictId: 42, District: 'MYAGDI', Province: 4, PlaceOfIssue: 'MYAGD' },
            { DistrictId: 43, District: 'NAWALPARASI', Province: 5, PlaceOfIssue: 'NAWAL' },
            { DistrictId: 44, District: 'PARBAT', Province: 4, PlaceOfIssue: 'PARBA' },
            { DistrictId: 45, District: 'SYANGJA', Province: 4, PlaceOfIssue: 'SYANG' },
            { DistrictId: 46, District: 'TANAHUN', Province: 4, PlaceOfIssue: 'TANAH' },
            { DistrictId: 47, District: 'NAWALPARASI EAST', Province: 4, PlaceOfIssue: 'NAWEA' },
            { DistrictId: 48, District: 'NAWALPARASI WEST', Province: 5, PlaceOfIssue: 'NAWWE' },
            { DistrictId: 49, District: 'ARGHAKHANCHI', Province: 5, PlaceOfIssue: 'ARGHA' },
            { DistrictId: 50, District: 'BANKE', Province: 5, PlaceOfIssue: 'BANKE' },
            { DistrictId: 51, District: 'BARDIYA', Province: 5, PlaceOfIssue: 'BARDI' },
            { DistrictId: 52, District: 'DANG', Province: 5, PlaceOfIssue: 'DANG' },
            { DistrictId: 53, District: 'GULMI', Province: 5, PlaceOfIssue: 'GULMI' },
            { DistrictId: 54, District: 'KAPILVASTU', Province: 5, PlaceOfIssue: 'KAPIL' },
            { DistrictId: 55, District: 'PALPA', Province: 5, PlaceOfIssue: 'PALPA' },
            { DistrictId: 56, District: 'PYUTHAN', Province: 5, PlaceOfIssue: 'PYUTH' },
            { DistrictId: 57, District: 'ROLPA', Province: 5, PlaceOfIssue: 'ROLPA' },
            { DistrictId: 58, District: 'RUKUM', Province: 5, PlaceOfIssue: 'RUKUM' },
            { DistrictId: 59, District: 'RUPANDEHI', Province: 5, PlaceOfIssue: 'RUPAN' },
            { DistrictId: 60, District: 'RUKUM EAST', Province: 5, PlaceOfIssue: 'RUKEA' },
            { DistrictId: 61, District: 'RUKUM WEST', Province: 6, PlaceOfIssue: 'RUKWE' },
            { DistrictId: 62, District: 'DAILEKH', Province: 6, PlaceOfIssue: 'DAILE' },
            { DistrictId: 63, District: 'DOLPA', Province: 6, PlaceOfIssue: 'DOLPA' },
            { DistrictId: 64, District: 'HUMLA', Province: 6, PlaceOfIssue: 'HUMLA' },
            { DistrictId: 65, District: 'JAJARKOT', Province: 6, PlaceOfIssue: 'JAJAR' },
            { DistrictId: 66, District: 'JUMLA', Province: 6, PlaceOfIssue: 'JUMLA' },
            { DistrictId: 67, District: 'KALIKOT', Province: 6, PlaceOfIssue: 'KALIK' },
            { DistrictId: 68, District: 'MUGU', Province: 6, PlaceOfIssue: 'MUGU' },
            { DistrictId: 69, District: 'SALYAN', Province: 6, PlaceOfIssue: 'SALYA' },
            { DistrictId: 70, District: 'SURKHET', Province: 6, PlaceOfIssue: 'SURKH' },
            { DistrictId: 71, District: 'ACHHAM', Province: 7, PlaceOfIssue: 'ACHHA' },
            { DistrictId: 72, District: 'BAITADI', Province: 7, PlaceOfIssue: 'BAITA' },
            { DistrictId: 73, District: 'BAJHANG', Province: 7, PlaceOfIssue: 'BAJHA' },
            { DistrictId: 74, District: 'BAJURA', Province: 7, PlaceOfIssue: 'BAJUR' },
            { DistrictId: 75, District: 'DADELDHURA', Province: 7, PlaceOfIssue: 'DADEL' },
            { DistrictId: 76, District: 'DARCHULA', Province: 7, PlaceOfIssue: 'DARCH' },
            { DistrictId: 77, District: 'DOTI', Province: 7, PlaceOfIssue: 'DOTI' },
            { DistrictId: 78, District: 'KAILALI', Province: 7, PlaceOfIssue: 'KAILA' },
            { DistrictId: 79, District: 'KANCHANPUR', Province: 7, PlaceOfIssue: 'KANCH' },
            { DistrictId: 80, District: 'NA', Province: 8, PlaceOfIssue: 'NA' },
        ];

        $scope.EducationLabelColl = [{ Id: 10, EducationLabel: 'No Formal Education' },
            { Id: 11, EducationLabel: 'Primary Education' },
            { Id: 12, EducationLabel: 'Secondary Education or Higher School' },
            { Id: 13, EducationLabel: 'Vocational Qualification' },
            { Id: 14, EducationLabel: 'Bachelor Degree' },
            { Id: 15, EducationLabel: 'Master Degree' },
            { Id: 16, EducationLabel: 'Doctorate or Higher' }
        ];

        $scope.SaluationColl = [{ Id: 1, Saluation: 'M/S' },
            { Id: 2, Saluation: 'MAST' },
            { Id: 3, Saluation: 'MISS' },
            { Id: 4, Saluation: 'MR' },
            { Id: 5, Saluation: 'MRS' }, 
           
        ];

        $scope.Countrycoll = [{ CountryId: 154, CountryName: 'Nepal' },
            { CountryId: 101, CountryName: 'India' },

        ];

        $scope.IncomeSourceColl = [{ Id: 1, SourceOfIncome: 'Own Business' },
            { Id: 2, SourceOfIncome: 'Salary/Employment' },
            { Id: 3, SourceOfIncome: 'Disposal of assets' },
            { Id: 4, SourceOfIncome: 'Inheritance/Gifts' },
            { Id: 5, SourceOfIncome: 'Commission' },
            { Id: 6, SourceOfIncome: 'Return of Investments' },
            { Id: 7, SourceOfIncome: 'Others' }
        ];

        $scope.PurposeofAccount = [{ Id: 1, Purpose: 'Savings' },
            { Id: 2, Purpose: 'Transactional' },
            { Id: 3, Purpose: 'Investment' },
            { Id: 4, Purpose: 'Remittance' },
            { Id: 5, Purpose: 'Payroll' },
            { Id: 6, Purpose: 'Share Investment/Demat' },
            { Id: 7, Purpose: 'Others' }
        ];


        $scope.IdSubmittedColl = [{ Id: 1, IdentifierType: 'CITIZENSHIP' },
            { Id: 2, IdentifierType: 'BIRTH_CERTIFICATE' },
            { Id: 3, IdentifierType: 'DRIVING LICENSE' },
            { Id: 4, IdentifierType: 'MINOR ID CARD' },
            { Id: 5, IdentifierType: 'PASSPORT CERTIFICATE' },
            { Id: 6, IdentifierType: 'VOTERS ID' },
            //{ Id: 7, IdentifierType: 'REFUGEE CARD' },
            { Id: 8, IdentifierType: 'INDIAN EMBASSY CARD' },
            { Id: 9, IdentifierType: 'ADHAR CARD' }
        ];

        $scope.MaritalStatusColl = [{ Id: 1, MaritalStatus: 'Married' },
            { Id: 2, MaritalStatus: 'Unmarried' },
            { Id: 3, MaritalStatus: 'Widow' },
            { Id: 4, MaritalStatus: 'Widower' },            
        ];

        $scope.ProvinceColl = [{ ProvinceId: 1, Province: 'KOSHI', CBSCode:'NPST1' },
            { ProvinceId: 2, Province: 'MADHESH', CBSCode: 'NPST2' },
            { ProvinceId: 3, Province: 'BAGMATI', CBSCode: 'NPST3' },
            { ProvinceId: 4, Province: 'GANDAKI', CBSCode: 'NPST4' },
            { ProvinceId: 5, Province: 'LUMBINI', CBSCode: 'NPST5' },
            { ProvinceId: 6, Province: 'KARNALI', CBSCode: 'NPST6' },
            { ProvinceId: 7, Province: 'SUDURPASCHIM', CBSCode: 'NPST7' },
            { ProvinceId: 8, Province: 'NOT APPLICABLE', CBSCode: 'NA' }
        ];
        $scope.RealtionList = [
            { Id: 1, text: 'FATHER' },
            { Id: 2, text: 'MOTHER' },
            { Id: 3, text: 'GRANDFATHER' },
            { Id: 4, text: 'GRANDMOTHER' },
            { Id: 5, text: 'HUSBAND' },
            { Id: 6, text: 'WIFE' },
            { Id: 7, text: 'BROTHER' },
            { Id: 8, text: 'SISTER' },
            { Id: 9, text: 'COUSIN' },
            { Id: 10, text: 'NEPHEW' },
            { Id: 11, text: 'NIECE' },
            { Id: 12, text: 'UNCLE' },
            { Id: 13, text: 'AUNT' },
            { Id: 14, text: 'SON' },
            { Id: 15, text: 'DAUGHTER' },
            { Id: 16, text: 'GRANDAUGHTER' },
            { Id: 17, text: 'GRANDSON' },
            { Id: 18, text: 'FATHER IN LAW' },
            { Id: 19, text: 'MOTHER IN LAW' },
            { Id: 20, text: 'SISTER IN LAW' },
            { Id: 21, text: 'DAUGHTER IN LAW' },
            { Id: 22, text: 'BROTHER IN LAW' },
            { Id: 23, text: 'SON IN LAW' },
            { Id: 24, text: 'FRIEND' },
            { Id: 25, text: 'ADVISOR' },
            { Id: 26, text: 'CHAIRMAN' },
            { Id: 27, text: 'DIRECTOR' },
            { Id: 28, text: 'EMPLOYEE' },
            { Id: 29, text: 'MANAGING' },
            { Id: 30, text: 'PARTNER' },
            { Id: 31, text: 'SHAREHOLDER' },
            { Id: 32, text: 'TRUSTEE ' },
            { Id: 33, text: 'FIANCE' },
            { Id: 34, text: 'NA' }
        ];

        $scope.currentPages = {
            BankAccount: 1,
        };

        $scope.searchData = {
            BankAccount: '',
        };

        $scope.perPage = {
            BankAccount: GlobalServices.getPerPageRow(),
        };     

        $scope.newBank = {
            For:2,
            BankId: 0,
            EmployeeType:0,
            InstantDebitCard: false,
            AnyAccount: 'no',
            Saluation: 4,
            FirstName: '',
            MiddleName: '',
            LastName: '',
            Gender: '',
            Nationality: 1,
            OtherNationality: '',
            DOB_TMP: '',
            Education: null,
            MaritalStatusId: 2,
            CitizenshipNumber: '',
            TypeofIdSubmittedId: null,
            IdNo: '',
            IDIssueDate: new Date(),
            IdIssuePlace: 0,
            IDExpiryDate: new Date(),
            MobileNo: '',
            TelephoneNo: '',
            Email: '',
            CA_HouseNo: '',
            CA_WardNo: 0,
            CA_StreetName: '',
            CA_ProvinceId: 0,
            CA_DistrictId: 0,
            CA_LocalLevelId: 0,
            CA_CountryId: 0,
            PA_HouseNo: '',
            PA_WardNo: 0,
            PA_StreetName: '',
            PA_ProvinceId: 0,
            PA_DistrictId: 0,
            PA_LocalLevelId: 0,
            PA_CountryId: 0,
            Occupation: 0,
            OtherOccupation: '',
            PanNo: '',
            OrganizationName: '',
            Designation: '',
            EmployeeContactNo: '',
            AnnualIncome: 0,
            SourceOfIncomeId: 7,
            PurposeofAccount: 1,
            OtherPurposeofAcc: '',
            AnnualTransaction: 0,
            SpouseName: '',
            FatherName: '',
            MotherName: '',
            GrandfatherName: '',
            GrandmotherName: '',
            SonName: '',
            DaughterName: '',
            DaughterInLawName: '',
            FatherInLawName: '',
            NomineeName: '',
            NomineeRelationId: null,
            NomineeIdentificationDocumentId: null,
            NomineeIdNo: '',
            NomineeFather: '',
            NomineeMother: '',
            InstantDebitCard: false,
            NameEmbossedDebitCard: false,
            MobBankingInquiryOnly: false,
            MobBankingInqAndTranBoth: false,
            InternetBankingInquiryOnly: false,
            InternetBankingInqAndTranBoth: false,
            Photo: null,
            PhotoPath: null,
            Signature: null,
            SignaturePath: null,
            UserId: '',
            AttachmentColl: [],
            SelectStudent: $scope.StudentSearchOptions[0].value,
            SelectEmployee: $scope.EmployeeSearchOptions[0].value,
            InstanctCard:'E',
            MobileBanking:10,
            InternetBanking:2,
            Mode: 'Save'
        };

        $scope.GetAllBankAccount();
     
    }
    $scope.SamePAddress = function () {

        if ($scope.newBank.IsSameAsPermanentAddress == true) {
            $scope.newBank.PA_HouseNo = $scope.newBank.CA_HouseNo;
            $scope.newBank.PA_ProvinceId = $scope.newBank.CA_ProvinceId;
            $scope.newBank.PA_DistrictId = $scope.newBank.CA_DistrictId;
            $scope.newBank.PA_LocalLevelId = $scope.newBank.CA_LocalLevelId;
            $scope.newBank.PA_WardNo = $scope.newBank.CA_WardNo;
            $scope.newBank.PA_StreetName = $scope.newBank.CA_StreetName;
            $scope.newBank.PA_CountryId = $scope.newBank.CA_CountryId;

        } else {
            $scope.newBank.PA_HouseNo = '';
            $scope.newBank.PA_ProvinceId = '';
            $scope.newBank.PA_DistrictId = '';
            $scope.newBank.PA_LocalLevelId = '';
            $scope.newBank.PA_WardNo = '';
            $scope.newBank.PA_StreetName = '';
            $scope.newBank.PA_CountryId = '';
        }
    }

    //***********Add Delete function for child Table 

    $scope.ClearPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newBank.PhotoData = null;
                $scope.newBank.Photo_TMP = [];
                $scope.newBank.PhotoPath = '';
            });

        });

        $('#imgPhoto1').attr('src', '');

    };

    $scope.ClearSPhoto = function () {
        $timeout(function () {
            $scope.$apply(function () {
                $scope.newBank.SPhotoData = null;
                $scope.newBank.SPhoto_TMP = [];
                $scope.newBank.SPhotoPath = '';
            });

        });

        $('#imgPhoto2').attr('src', '');

    };

    $scope.delDocunemtDet = function (ind) {
        if ($scope.newBank.AttachmentColl) {
            if ($scope.newBank.AttachmentColl.length > 0) {
                $scope.newBank.AttachmentColl.splice(ind, 1);
            }
        }
    };

    $scope.ShowPersonalImg = function (docDet) {
        $scope.viewImg = {
            DocPath: '',
            File: null,
            FileData: null
        };
        if (docDet.attachFile || docDet.File) {
            $scope.viewImg.DocPath = docDet.attachFile;
            $scope.viewImg.File = docDet.File;
            $scope.viewImg.FileData = docDet.DocumentData;
            $('#PersonalImg').modal('show');
        } else
            Swal.fire('No File Found');
    };


    $scope.AddMoreFiles = function (files, docType, des) {
        if (files && docType) {
            if (files != null && docType != null) {
                angular.forEach(files, function (file) {
                    $scope.newBank.AttachmentColl.push({
                        DocumentTypeId: docType.Id,
                        DocumentTypeName: docType.IdentifierType,
                        File: file,
                        Name: file.name,
                        Type: file.type,
                        Size: file.size,
                        Description: des,
                        Path: null
                    });
                })
                $scope.docType = null;
                $scope.attachFile = null;
                $scope.docDescription = '';

                $('#flMoreFiles').val('');
            }
            else {
                alert('Document Type or Document in missing ')
            }
        }
    };

    $scope.delAttachmentFiles = function (ind) {
        if ($scope.newBank.AttachmentColl) {
            if ($scope.newBank.AttachmentColl.length > 0) {
                $scope.newBank.AttachmentColl.splice(ind, 1);
            }
        }
    };



    $scope.CloseModal = function () {
        $('#modal-xl').modal('hide');
        $('#PersonalImg').modal('hide');
    }
       
    //***********Add Delete function for child Table

    $scope.ClearBankAccount = function () {
        $scope.ClearPhoto();
        $scope.ClearSPhoto();

        $timeout(function () {
            $scope.newBank = {
                EmployeeType:0,
                BankId: 0,
                InstantDebitCard: false,
                AnyAccount: 'no',
                Saluation: 4,
                FirstName: '',
                MiddleName: '',
                LastName: '',
                Gender: '',
                Nationality: 1,
                OtherNationality: '',
                DOB_TMP: '',
                Education: null,
                MaritalStatusId: 2,
                CitizenshipNumber: '',
                TypeofIdSubmittedId: null,
                IdNo: '',
                IDIssueDate: new Date(),
                IdIssuePlace: 0,
                IDExpiryDate: new Date(),
                MobileNo: '',
                TelephoneNo: '',
                Email: '',
                CA_HouseNo: '',
                CA_WardNo: 0,
                CA_StreetName: '',
                CA_ProvinceId: 0,
                CA_DistrictId: 0,
                CA_LocalLevelId: 0,
                CA_CountryId: 0,
                PA_HouseNo: '',
                PA_WardNo: 0,
                PA_StreetName: '',
                PA_ProvinceId: 0,
                PA_DistrictId: 0,
                PA_LocalLevelId: 0,
                PA_CountryId: 0,
                Occupation: 0,
                OtherOccupation: '',
                PanNo: '',
                OrganizationName: '',
                Designation: '',
                EmployeeContactNo: '',
                AnnualIncome: 0,
                SourceOfIncomeId: 7,
                PurposeofAccount: 1,
                OtherPurposeofAcc: '',
                AnnualTransaction: 0,
                SpouseName: '',
                FatherName: '',
                MotherName: '',
                GrandfatherName: '',
                GrandmotherName: '',
                SonName: '',
                DaughterName: '',
                DaughterInLawName: '',
                FatherInLawName: '',
                NomineeName: '',
                NomineeRelationId: null,
                NomineeIdentificationDocumentId: null,
                NomineeIdNo: '',
                NomineeFather: '',
                NomineeMother: '',
                InstantDebitCard: false,
                NameEmbossedDebitCard: false,
                MobBankingInquiryOnly: false,
                MobBankingInqAndTranBoth: false,
                InternetBankingInquiryOnly: false,
                InternetBankingInqAndTranBoth: false,
                Photo: null,
                PhotoPath: null,
                Signature: null,
                SignaturePath: null,
                UserId: '',
                AttachmentColl: [],
                SelectStudent: $scope.StudentSearchOptions[0].value,
                SelectEmployee: $scope.EmployeeSearchOptions[0].value,
                InstanctCard: 'E',
                MobileBanking: 10,
                InternetBanking: 2,
                Mode: 'Save'
            };           
        });
    }



    //*********************************CRUDE Start For BankAccount
    $scope.IsValidBankAccount = function () {

        if ($scope.newBank.Saluation == undefined || $scope.newBank.Saluation < 1) {
            Swal.fire('Please ! Select Saluation')
            return false;
        }
        if ($scope.newBank.FirstName == undefined || $scope.newBank.FirstName.isEmpty()) {
            Swal.fire('Please ! Enter FirstName')
            return false;
        }

        if ($scope.newBank.LastName == undefined || $scope.newBank.LastName.isEmpty()) {
            Swal.fire('Please ! Enter Lastname')
            return false;
        }
        if ($scope.newBank.CitizenshipNumber == undefined || $scope.newBank.CitizenshipNumber.isEmpty()) {
            Swal.fire('Please ! Enter CitizenshipNo')
            return false;
        }

        if ($scope.newBank.IdNo == undefined || $scope.newBank.IdNo.isEmpty()) {
            Swal.fire('Please ! Enter IdNo')
            return false;
        }
        if ($scope.newBank.MobileNo == undefined || $scope.newBank.MobileNo.isEmpty()) {
            Swal.fire('Please ! Enter ContactNo')
            return false;
        }
        if ($scope.newBank.Email == undefined || $scope.newBank.Email.isEmpty()) {
            Swal.fire('Please ! Enter Email')
            return false;
        }
        //if ($scope.newBank.CA_WardNo.isEmpty()) {
        //    Swal.fire('Please ! Enter Ward No of Current Address')
        //    return false;
        //}
        if ($scope.newBank.CA_StreetName == undefined || $scope.newBank.CA_StreetName.isEmpty()) {
            Swal.fire('Please ! Enter Streetname of Current Address')
            return false;
        }

        //if ($scope.newBank.PA_WardNo.isEmpty()) {
        //    Swal.fire('Please ! Enter Ward No of Permanent Address')
        //    return false;
        //}
        if ($scope.newBank.PA_StreetName == undefined || $scope.newBank.PA_StreetName.isEmpty()) {
            Swal.fire('Please ! Enter Streetname of Permanent Address')
            return false;
        }
        if ($scope.newBank.PanNo ==undefined ||  $scope.newBank.PanNo.isEmpty()) {
            Swal.fire('Please ! Enter PanNo')
            return false;
        }

        if ($scope.newBank.FatherName == undefined || $scope.newBank.FatherName.isEmpty()) {
            Swal.fire('Please ! Enter Father Name')
            return false;
        }
        if ($scope.newBank.MotherName == undefined || $scope.newBank.MotherName.isEmpty()) {
            Swal.fire('Please ! Enter Mother Name')
            return false;
        }
        if ($scope.newBank.GrandfatherName == undefined || $scope.newBank.GrandfatherName.isEmpty()) {
            Swal.fire('Please ! Enter Grandfather Name')
            return false;
        }
        
        return true;
    }

    $scope.SaveUpdateBankAccount = function () {
        if ($scope.IsValidBankAccount() == true) {
            if ($scope.confirmMSG.Accept == true) {
                var saveModify = $scope.newBank.Mode;
                Swal.fire({
                    title: 'Do you want to ' + saveModify + ' the current data?',
                    showCancelButton: true,
                    confirmButtonText: saveModify,
                }).then((result) => {

                    if (result.isConfirmed) {
                        $scope.CallSaveUpdateBankAccount();
                    }
                });
            } else
                $scope.CallSaveUpdateBankAccount();
        }
    };

    $scope.CallSaveUpdateBankAccount = function () {
        $scope.loadingstatus = "running";
        showPleaseWait();
        var UserPhoto = $scope.newBank.Photo_TMP;
        var Signature = $scope.newBank.SPhoto_TMP;
        var filesColl = $scope.newBank.AttachmentColl;

        if ($scope.newBank.DOBDet) {
            $scope.newBank.DOB = $filter('date')(new Date($scope.newBank.DOBDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newBank.DOB = $filter('date')(new Date(), 'yyyy-MM-dd');

        if ($scope.newBank.IDIssueDateDet) {
            $scope.newBank.IDIssueDate = $filter('date')(new Date($scope.newBank.IDIssueDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newBank.IDIssueDate = $filter('date')(new Date(), 'yyyy-MM-dd');

        if ($scope.newBank.IDExpiryDateDet) {
            $scope.newBank.IDExpiryDate = $filter('date')(new Date($scope.newBank.IDExpiryDateDet.dateAD), 'yyyy-MM-dd');
        } else
            $scope.newBank.IDExpiryDate = $filter('date')(new Date(), 'yyyy-MM-dd');


        $scope.newBank.DocumentDetColl = [];
        $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/SaveNewBankAccount",
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var formData = new FormData();
                formData.append("jsonData", angular.toJson(data.jsonData));

                if (data.UsPhoto && data.UsPhoto.length > 0)
                    formData.append("UserPhoto", data.UsPhoto[0]);

                if (data.SiPhoto && data.SiPhoto.length > 0)
                    formData.append("Signature", data.SiPhoto[0]);

                if (data.files) {
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i].File);
                    }
                }
                return formData;
            },
            data: { jsonData: $scope.newBank, files: filesColl, UsPhoto: UserPhoto, SiPhoto: Signature }
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            Swal.fire(res.data.ResponseMSG);
            if (res.data.IsSuccess == true) {
                $scope.ClearBankAccount();
                $scope.GetAllBankAccount();
            }
        }, function (errormessage) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
        });
    }

    $scope.GetAllBankAccount = function () {
        $scope.BankAccountList = [];
        $http({
            method: 'GET',
            url: base_url + "Academic/Transaction/GetAllNewBankAccount",
            dataType: "json"
        }).then(function (res) {
            hidePleaseWait();
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {
                $scope.BankAccountList = res.data.Data;
            } else {
                Swal.fire(res.data.ResponseMSG);
            }
        }, function (reason) {
            Swal.fire('Failed' + reason);
        });
    }
    

    $scope.DelBankAccountById = function (refData, ind) {
        Swal.fire({
            title: 'Are you sure you want to delete ' + refData.FirstName + '?',
            showCancelButton: true,
            confirmButtonText: 'Delete',
        }).then((result) => {
            if (result.isConfirmed) {
                var para = { BankId: refData.BankId };
                $http({
                    method: 'POST',
                    url: base_url + "Academic/Transaction/DeleteNewBankAccount",
                    dataType: "json",
                    data: JSON.stringify(para)
                }).then(function (res) {
                    hidePleaseWait();
                    $scope.loadingStatus = "stop";
                     
                    Swal.fire(res.data.ResponseMSG);
                    if (res.data.IsSuccess == true) {
                        $scope.BankAccountList.splice(ind, 1);
                    }
                }, function (reason) {
                    Swal.fire('Failed' + reason);
                });
            }

        });
    }

    $scope.GetBankAccountById = function (beData) {
        $scope.loadingstatus = "running";
        var para = {
            BankId: beData.BankId
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/getNewBankAccountById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            if (res.data.IsSuccess && res.data.Data) {              
                    $scope.newBank = res.data.Data;
                     $scope.newBank.AnyAccount = 'no';

                if ($scope.newBank.DOB) {
                    $scope.newBank.DOB_TMP = new Date($scope.newBank.DOB);
                    $scope.newBank.DOB_TMP_AD = new Date($scope.newBank.DOB);
                }
                    

                if ($scope.newBank.IDIssueDate) {
                    $scope.newBank.IDIssueDate_TMP = new Date($scope.newBank.IDIssueDate);
                    $scope.newBank.IDIssueDate_TMP_AD = new Date($scope.newBank.IDIssueDate);
                }
                    

                if ($scope.newBank.IDExpiryDate) {
                    $scope.newBank.IDExpiryDate_TMP = new Date($scope.newBank.IDExpiryDate);
                    $scope.newBank.IDExpiryDate_TMP_AD = new Date($scope.newBank.IDExpiryDate);
                }
                    


                if ($scope.newBank.IdIssuePlace && $scope.newBank.IdIssuePlace.length > 0)
                    $scope.newBank.IdIssuePlace = parseInt($scope.newBank.IdIssuePlace);

                if ($scope.newBank.Occupation && $scope.newBank.Occupation.length > 0)
                    $scope.newBank.Occupation = parseInt($scope.newBank.Occupation);

                    $scope.newBank.Mode = 'Modify';
                    $('.nav-tabs a:first').tab('show');
              

            } else
                Swal.fire(res.data.ResponseMSG);


        }, function (reason) {
            alert('Failed' + reason);
        });
    }

    $scope.ChangeStudent = function (studentId) {

        if (!studentId || studentId == 0)
            return;

        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            StudentId: studentId
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/GetStudentById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            if (res.data.IsSuccess && res.data.Data) {
                var st = res.data.Data;
                $scope.newBank.FirstName = st.FirstName;
                $scope.newBank.MiddleName = st.MiddleName;
                $scope.newBank.LastName = st.LastName;
                $scope.newBank.CitizenshipNumber = st.CitizenshipNo;

                $scope.newBank.TelephoneNo = st.EC_Phone;
                if (st.Gender == 1) {
                    $scope.newBank.Saluation = 4;
                    $scope.newBank.Gender = 'M';
                }
                else if (st.Gender == 2) {
                    $scope.newBank.Saluation = 5;
                    $scope.newBank.Gender = 'F';
                }
                else {
                    $scope.newBank.Saluation = 4;
                    $scope.newBank.Gender = 'M';
                }

                //Gender,
                //$scope.newBank.Nationality = st.Nationality;
                $scope.newBank.Nationality = 1;
                $scope.newBank.PhotoPath = st.PhotoPath;
                $scope.newBank.SignaturePath = st.SignaturePath;

                $scope.newBank.MobileNo = st.ContactNo;
                $scope.newBank.Email = st.Email;
                $scope.newBank.CA_HouseNo = st.CA_HouseNo;
                $scope.newBank.CA_StreetName = st.CA_StreetName;

                $scope.newBank.PA_HouseNo = st.PA_HouseNo;
                $scope.newBank.PA_StreetName = st.PA_StreetName;

                $scope.newBank.FatherName = st.FatherName;
                $scope.newBank.MotherName = st.MotherName;

                $scope.newBank.GrandfatherName = st.GrandFather;
                $scope.newBank.GrandmotherName = st.GrandmotherName;

                $scope.newBank.StudentId = st.StudentId;
                $scope.newBank.EmployeeId = null;
                $scope.newBank.SpouseName = st.SpouseName;

                $scope.newBank.CA_StreetName = st.CA_FullAddress;
                $scope.newBank.CA_WardNo = st.CA_Ward;
                $scope.newBank.CA_HouseNo = st.CA_HouseNo;

                $scope.newBank.PA_StreetName = st.PA_FullAddress;
                $scope.newBank.PA_WardNo = st.PA_Ward;
                $scope.newBank.PA_HouseNo = st.PA_HouseNo;

                $scope.newBank.PanNo = st.PanId;

                if (st.DOB_AD)
                    $scope.newBank.DOB_TMP = new Date(st.DOB_AD);

            } else
                Swal.fire(res.data.ResponseMSG);


        }, function (reason) {
            alert('Failed' + reason);
        });
    }

    $scope.ChangeDOBAD = function (col) {

        if (col == 'ad') {
            if ($scope.newBank.DOB_TMP_AD) {
                var dt = new Date($scope.newBank.DOB_TMP_AD);

                $timeout(function () {
                    $scope.newBank.DOB_TMP = dt;
                });
                
            }
            else
                $scope.newBank.DOB_TMP = null;
        } else if (col == 'bs') {
            if ($scope.newBank.DOBDet) {
                var dt = new Date($scope.newBank.DOBDet.dateAD);

                $timeout(function () {
                    $scope.newBank.DOB_TMP_AD = dt;
                });
                
            }
            else
                $scope.newBank.DOB_TMP_AD = null;
        }
        
    }

    $scope.ChangeIDIssueAD = function (col) {
        if (col == 'ad') {
            if ($scope.newBank.IDIssueDate_TMP_AD) {
                var dt = new Date($scope.newBank.IDIssueDate_TMP_AD);

                $timeout(function () {
                    $scope.newBank.IDIssueDate_TMP = dt;
                });

            }
            else
                $scope.newBank.IDIssueDate_TMP = null;
        } else if (col == 'bs') {
            if ($scope.newBank.IDIssueDateDet) {
                var dt = new Date($scope.newBank.IDIssueDateDet.dateAD);

                $timeout(function () {
                    $scope.newBank.IDIssueDate_TMP_AD = dt;
                });

            }
            else
                $scope.newBank.IDIssueDate_TMP_AD = null;
        }
    }

    $scope.ChangeIDEXPAD = function (col) {
        if (col == 'ad') {
            if ($scope.newBank.IDExpiryDate_TMP_AD) {
                var dt = new Date($scope.newBank.IDExpiryDate_TMP_AD);

                $timeout(function () {
                    $scope.newBank.IDExpiryDate_TMP = dt;
                });

            }
            else
                $scope.newBank.IDExpiryDate_TMP = null;
        } else if (col == 'bs') {
            if ($scope.newBank.IDExpiryDateDet) {
                var dt = new Date($scope.newBank.IDExpiryDateDet.dateAD);

                $timeout(function () {
                    $scope.newBank.IDExpiryDate_TMP_AD = dt;
                });

            }
            else
                $scope.newBank.IDExpiryDate_TMP_AD = null;
        }
    }

    $scope.ChangeEmployee = function (employeeId) {

        if (!employeeId || employeeId == 0)
            return;

        $scope.loadingstatus = "running";
        showPleaseWait();
        var para = {
            EmployeeId: employeeId
        };
        $http({
            method: 'POST',
            url: base_url + "Academic/Transaction/GetEmployeeById",
            dataType: "json",
            data: JSON.stringify(para)
        }).then(function (res) {
            $scope.loadingstatus = "stop";
            hidePleaseWait();
            if (res.data.IsSuccess && res.data.Data) {
                var st = res.data.Data;
                $scope.newBank.FirstName = st.FirstName;
                $scope.newBank.MiddleName = st.MiddleName;
                $scope.newBank.LastName = st.LastName;                
                $scope.newBank.CitizenshipNumber = st.CitizenshipNo;
                
                $scope.newBank.TelephoneNo= st.EC_Phone;
                if (st.Gender == 1) {
                    $scope.newBank.Saluation = 4;
                    $scope.newBank.Gender = 'M';
                }
                else if (st.Gender == 2) {
                    $scope.newBank.Saluation = 5;
                    $scope.newBank.Gender = 'F';
                }
                else {
                    $scope.newBank.Saluation = 4;
                    $scope.newBank.Gender = 'M';
                } 

                //Gender,
                //$scope.newBank.Nationality = st.Nationality;
                $scope.newBank.Nationality = 1;
                $scope.newBank.PhotoPath = st.PhotoPath;
                $scope.newBank.SignaturePath = st.SignaturePath;

                $scope.newBank.MobileNo = st.PersnalContactNo;
                $scope.newBank.Email = st.EmailId;
                $scope.newBank.CA_HouseNo = st.CA_HouseNo;
                $scope.newBank.CA_StreetName = st.CA_StreetName;

                $scope.newBank.PA_HouseNo = st.PA_HouseNo;
                $scope.newBank.PA_StreetName = st.PA_StreetName;

                $scope.newBank.FatherName = st.FatherName;
                $scope.newBank.MotherName = st.MotherName;

                $scope.newBank.GrandfatherName = st.GrandFather;
                $scope.newBank.GrandmotherName = st.GrandmotherName;

                $scope.newBank.StudentId = null;
                $scope.newBank.EmployeeId = st.EmployeeId;
                $scope.newBank.SpouseName = st.SpouseName;

                $scope.newBank.CA_StreetName = st.TA_FullAddress;                
                $scope.newBank.CA_WardNo = st.TA_Ward;
                $scope.newBank.CA_HouseNo = st.TA_HouseNo;

                $scope.newBank.PA_StreetName = st.PA_FullAddress;
                $scope.newBank.PA_WardNo = st.PA_Ward;
                $scope.newBank.PA_HouseNo = st.PA_HouseNo;

                $scope.newBank.PanNo = st.PanId;

                if (st.DOB_AD)
                    $scope.newBank.DOB_TMP = new Date(st.DOB_AD);

            } else
                Swal.fire(res.data.ResponseMSG);


        }, function (reason) {
            alert('Failed' + reason);
        });
    }
});