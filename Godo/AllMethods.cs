﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godo
{
    public class AllMethods
    {

        // This converts little endian values to int
        public static int GetLittleEndianInt(byte[] data, int startIndex)
        {
            return (data[startIndex + 3] << 24)
                 | (data[startIndex + 2] << 16)
                 | (data[startIndex + 1] << 8)
                 | data[startIndex];
        }

        public static int GetLittleEndianIntTwofer(byte[] data, int startIndex)
        {
            return (data[startIndex + 1] << 8)
                 | data[startIndex];
        }

        public static int GetPreviousLittleEndianInt(byte[] data, int startIndex)
        {
            return (data[startIndex - 1] << 24)
                 | (data[startIndex - 2] << 16)
                 | (data[startIndex - 3] << 8)
                 | data[startIndex - 4];
        }

        // This converts a ulong value (16-bit number) to a 2-byte little endian value (8-bit per byte)
        public static byte[] GetLittleEndianConvert(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            // If it was big endian, reverse it
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }

        public static byte[] AddLittleEndian(byte[] a, byte[] b)
        {
            List<byte> result = new List<byte>();
            if (a.Length < b.Length)
            {
                byte[] t = a;
                a = b;
                b = t;
            }
            int carry = 0;
            for (int i = 0; i < b.Length; ++i)
            {
                int sum = a[i] + b[i] + carry;
                result.Add((byte)(sum & 0xFF));
                carry = sum >> 8;
            }
            for (int i = b.Length; i < a.Length; ++i)
            {
                int sum = a[i] + carry;
                result.Add((byte)(sum & 0xFF));
                carry = sum >> 8;
            }
            if (carry > 0)
            {
                result.Add((byte)carry);
            }
            return result.ToArray();
        }

        public static bool CheckExcludedModel(ulong modelID)
        {
            // List of excluded models from random allocation
            ulong[] excluded = { 14, 15, 38, 48, 49, 50, 54, 64, 68, 70, 78, 92, 95, 97, 133, 139, 149, 150, 151, 152, 153,
                154, 155, 156, 198, 212, 213, 214, 228, 229, 236, 240, 241, 242, 243, 254, 255, 256, 258, 259, 260,
                288, 294, 296, 297, 298, 299, 300, 306, 307, 308, 311, 312, 332 };

            if (excluded.Contains(modelID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckAnimSet(ulong modelID)
        {
            // Has alternate idle + damaged anims
            ulong[] animSet = { 86, 131, 143, 147, 170, 202, 278, 339, 340, 341, 342, 343, 344, 347, 349, 350 };

            if (animSet.Contains(modelID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckBossSet(ulong modelID)
        {
            // Bosses with alternate idle/damaged anims
            ulong[] bossSet = { 10, 11, 22, 33, 37, 71, 81, 195 };

            if (bossSet.Contains(modelID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckExcludedScene(int sceneID)
        {
            // List of excluded scenes from random allocation
            int[] excluded = { 12, 21, 31, 32, 33, 34, 35, 36, 48, 81, 91, 96, 97, 101, 102, 103, 104, 105, 106, 107, 108, 114, 115, 116, 117, 120, 131, 133, 138, 139, 143, 144, 150, 174, 185, 186, 189, 194, 195, 205, 209, 210, 211, 214, 215, 217, 221, 223, 224, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 237, 243, 245, 246 };
            if (excluded.Contains(sceneID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int CheckValidMateriaIndex(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                picker = rnd.Next(91);
                switch (picker)
                {
                    // Invalid Materia; no data
                    case 22:
                        break;

                    case 38:
                        break;

                    case 45:
                        break;

                    case 46:
                        break;

                    case 47:
                        break;

                    case 63:
                        break;

                    case 66:
                        break;

                    case 67:
                        break;

                    // OP Materia: KOTR and Master Command/Magic/Summon
                    case 48:
                        break;

                    case 73:
                        break;

                    case 89:
                        break;

                    case 90:
                        break;

                    default:
                        valid = true;
                        break;
                }
            }
            return picker;
        }

        public static int CheckValidLimitIndex(Random rnd)
        {
            bool valid = false;
            int picker = 0;
            while (valid == false)
            {
                picker = rnd.Next(127, 197);
                switch (picker)
                {
                    // Invalid Materia; no data
                    case 149:
                        break;

                    case 150:
                        break;

                    case 151:
                        break;

                    case 152:
                        break;

                    case 153:
                        break;

                    case 154:
                        break;

                    case 155:
                        break;

                    case 171:
                        break;

                    case 172:
                        break;

                    case 173:
                        break;

                    case 174:
                        break;

                    case 175:
                        break;

                    case 176:
                        break;

                    case 188:
                        break;

                    case 189:
                        break;

                    default:
                        valid = true;
                        break;
                }
            }
            return picker;
        }

        // This method generates a random name using two 4-letter words
        public static byte[] NameGenerate(Random rnd)
        {
            string[] nameArray = {"AAHS", "AALS", "ABAC", "ABAS", "ABBA", "ABBE", "ABBS", "ABED", "ABET", "ABID", "ABLE", "ABLY", "ABOS", "ABRI", "ABUT", "ABYE", "ABYS", "ACAI", "ACCA", "ACED", "ACER", "ACES", "ACHE", "ACHY", "ACID", "ACME", "ACNE", "ACRE", "ACTA", "ACTS", "ACYL", "ADAW", "ADDS", "ADDY", "ADIT", "ADOS", "ADRY", "ADZE", "AEON", "AERO", "AERY", "AESC", "AFAR", "AFFY", "AFRO", "AGAR", "AGAS", "AGED", "AGEE", "AGEN", "AGER", "AGES", "AGHA", "AGIN", "AGIO", "AGLU", "AGLY", "AGMA", "AGOG", "AGON", "AGUE", "AHED", "AHEM", "AHIS", "AHOY", "AIAS", "AIDE", "AIDS", "AIGA", "AILS", "AIMS", "AINE", "AINS", "AIRN", "AIRS", "AIRT", "AIRY", "AITS", "AITU", "AJAR", "AJEE", "AKED", "AKEE", "AKES", "AKIN", "ALAE", "ALAN", "ALAP", "ALAR", "ALAS", "ALAY", "ALBA", "ALBE", "ALBS", "ALCO", "ALEC", "ALEE", "ALEF", "ALES", "ALEW", "ALFA", "ALFS", "ALGA", "ALIF", "ALIT", "ALKO", "ALKY", "ALLS", "ALLY", "ALMA", "ALME", "ALMS", "ALOD", "ALOE", "ALOW", "ALPS", "ALSO", "ALTO", "ALTS", "ALUM", "AMAH", "AMAS", "AMBO", "AMEN", "AMIA", "AMID", "AMIE", "AMIN", "AMIR", "AMIS", "AMLA", "AMMO", "AMOK", "AMPS", "AMUS", "AMYL", "ANAL", "ANAN", "ANAS", "ANCE", "ANDS", "ANES", "ANEW", "ANGA", "ANIL", "ANIS", "ANKH", "ANNA", "ANNO", "ANNS", "ANOA", "ANON", "ANOW", "ANSA", "ANTA", "ANTE", "ANTI", "ANTS", "ANUS", "APAY", "APED", "APER", "APES", "APEX", "APOD", "APOS", "APPS", "APSE", "APSO", "APTS", "AQUA", "ARAK", "ARAR", "ARBA", "ARBS", "ARCH", "ARCO", "ARCS", "ARDS", "AREA", "ARED", "AREG", "ARES", "ARET", "AREW", "ARFS", "ARIA", "ARID", "ARIL", "ARIS", "ARKS", "ARLE", "ARMS", "ARMY", "ARNA", "AROW", "ARPA", "ARSE", "ARSY", "ARTI", "ARTS", "ARTY", "ARUM", "ARVO", "ARYL", "ASAR", "ASCI", "ASEA", "ASHY", "ASKS", "ASPS", "ATAP", "ATES", "ATMA", "ATOC", "ATOK", "ATOM", "ATOP", "ATUA", "AUFS", "AUKS", "AULA", "AULD", "AUNE", "AUNT", "AURA", "AUTO", "AVAL", "AVAS", "AVEL", "AVER", "AVES", "AVID", "AVOS", "AVOW", "AWAY", "AWDL", "AWED", "AWEE", "AWES", "AWLS", "AWNS", "AWNY", "AWOL", "AWRY", "AXAL", "AXED", "AXEL", "AXES", "AXIL", "AXIS", "AXLE", "AXON", "AYAH", "AYES", "AYIN", "AYRE", "AYUS", "AZAN", "AZON", "AZYM", "BAAL", "BAAS", "BABA", "BABE", "BABU", "BABY", "BACH", "BACK", "BACS", "BADE", "BADS", "BAEL", "BAFF", "BAFT", "BAGH", "BAGS", "BAHT", "BAIL", "BAIT", "BAJU", "BAKE", "BALD", "BALE", "BALK", "BALL", "BALM", "BALS", "BALU", "BAMS", "BANC", "BAND", "BANE", "BANG", "BANI", "BANK", "BANS", "BANT", "BAPS", "BAPU", "BARB", "BARD", "BARE", "BARF", "BARK", "BARM", "BARN", "BARP", "BARS", "BASE", "BASH", "BASK", "BASS", "BAST", "BATE", "BATH", "BATS", "BATT", "BAUD", "BAUK", "BAUR", "BAWD", "BAWL", "BAWN", "BAWR", "BAYE", "BAYS", "BAYT", "BEAD", "BEAK", "BEAM", "BEAN", "BEAR", "BEAT", "BEAU", "BECK", "BEDE", "BEDS", "BEDU", "BEEF", "BEEN", "BEEP", "BEER", "BEES", "BEET", "BEGO", "BEGS", "BEIN", "BELL", "BELS", "BELT", "BEMA", "BEND", "BENE", "BENI", "BENJ", "BENS", "BENT", "BERE", "BERG", "BERK", "BERM", "BEST", "BETA", "BETE", "BETH", "BETS", "BEVY", "BEYS", "BHAT", "BHEL", "BHUT", "BIAS", "BIBB", "BIBS", "BICE", "BIDE", "BIDI", "BIDS", "BIEN", "BIER", "BIFF", "BIGA", "BIGG", "BIGS", "BIKE", "BILE", "BILK", "BILL", "BIMA", "BIND", "BINE", "BING", "BINK", "BINS", "BINT", "BIOG", "BIOS", "BIRD", "BIRK", "BIRL", "BIRO", "BIRR", "BISE", "BISH", "BISK", "BIST", "BITE", "BITO", "BITS", "BITT", "BIZE", "BLAB", "BLAD", "BLAE", "BLAG", "BLAH", "BLAM", "BLAT", "BLAW", "BLAY", "BLEB", "BLED", "BLEE", "BLET", "BLEW", "BLEY", "BLIN", "BLIP", "BLOB", "BLOC", "BLOG", "BLOT", "BLOW", "BLUB", "BLUE", "BLUR", "BOAB", "BOAK", "BOAR", "BOAS", "BOAT", "BOBA", "BOBS", "BOCK", "BODE", "BODS", "BODY", "BOEP", "BOET", "BOFF", "BOGS", "BOGY", "BOHO", "BOHS", "BOIL", "BOIS", "BOKE", "BOKO", "BOKS", "BOLA", "BOLD", "BOLE", "BOLL", "BOLO", "BOLT", "BOMA", "BOMB", "BONA", "BOND", "BONE", "BONG", "BONK", "BONY", "BOOB", "BOOH", "BOOK", "BOOL", "BOOM", "BOON", "BOOR", "BOOS", "BOOT", "BOPS", "BORA", "BORD", "BORE", "BORK", "BORM", "BORN", "BORS", "BORT", "BOSH", "BOSK", "BOSS", "BOTA", "BOTH", "BOTS", "BOTT", "BOUK", "BOUN", "BOUT", "BOWL", "BOWR", "BOWS", "BOXY", "BOYF", "BOYG", "BOYO", "BOYS", "BOZO", "BRAD", "BRAE", "BRAG", "BRAK", "BRAN", "BRAS", "BRAT", "BRAW", "BRAY", "BRED", "BREE", "BREI", "BREN", "BRER", "BREW", "BREY", "BRIE", "BRIG", "BRIK", "BRIM", "BRIN", "BRIO", "BRIS", "BRIT", "BROD", "BROG", "BROO", "BROS", "BROW", "BRRR", "BRUS", "BRUT", "BRUX", "BUAT", "BUBA", "BUBO", "BUBS", "BUBU", "BUCK", "BUDA", "BUDI", "BUDO", "BUDS", "BUFF", "BUFO", "BUGS", "BUHL", "BUHR", "BUIK", "BUKE", "BULB", "BULK", "BULL", "BUMF", "BUMP", "BUMS", "BUNA", "BUND", "BUNG", "BUNK", "BUNN", "BUNS", "BUNT", "BUOY", "BURA", "BURB", "BURD", "BURG", "BURK", "BURL", "BURN", "BURP", "BURR", "BURS", "BURY", "BUSH", "BUSK", "BUSS", "BUST", "BUSY", "BUTE", "BUTS", "BUTT", "BUYS", "BUZZ", "BYDE", "BYES", "BYKE", "BYRE", "BYRL", "BYTE", "CAAS", "CABA", "CABS", "CACA", "CADE", "CADI", "CADS", "CAFE", "CAFF", "CAGE", "CAGS", "CAGY", "CAID", "CAIN", "CAKE", "CAKY", "CALF", "CALK", "CALL", "CALM", "CALO", "CALP", "CALX", "CAMA", "CAME", "CAMO", "CAMP", "CAMS", "CANE", "CANG", "CANN", "CANS", "CANT", "CANY", "CAPA", "CAPE", "CAPH", "CAPI", "CAPO", "CAPS", "CARB", "CARD", "CARE", "CARK", "CARL", "CARN", "CARP", "CARR", "CARS", "CART", "CASA", "CASE", "CASH", "CASK", "CAST", "CATE", "CATS", "CAUF", "CAUK", "CAUL", "CAUM", "CAUP", "CAVA", "CAVE", "CAVY", "CAWK", "CAWS", "CAYS", "CEAS", "CECA", "CEDE", "CEDI", "CEES", "CEIL", "CELL", "CELS", "CELT", "CENS", "CENT", "CEPE", "CEPS", "CERE", "CERO", "CERT", "CESS", "CETE", "CHAD", "CHAI", "CHAL", "CHAM", "CHAO", "CHAP", "CHAR", "CHAS", "CHAT", "CHAV", "CHAW", "CHAY", "CHEF", "CHER", "CHEW", "CHEZ", "CHIA", "CHIB", "CHIC", "CHID", "CHIK", "CHIN", "CHIP", "CHIS", "CHIT", "CHIV", "CHIZ", "CHOC", "CHOG", "CHON", "CHOP", "CHOU", "CHOW", "CHUB", "CHUG", "CHUM", "CHUT", "CIAO", "CIDE", "CIDS", "CIEL", "CIGS", "CILL", "CINE", "CION", "CIRE", "CIRL", "CIST", "CITE", "CITO", "CITS", "CITY", "CIVE", "CLAD", "CLAG", "CLAM", "CLAN", "CLAP", "CLAT", "CLAW", "CLAY", "CLEF", "CLEG", "CLEM", "CLEW", "CLIP", "CLOD", "CLOG", "CLON", "CLOP", "CLOT", "CLOU", "CLOW", "CLOY", "CLUB", "CLUE", "COAL", "COAT", "COAX", "COBB", "COBS", "COCA", "COCH", "COCK", "COCO", "CODA", "CODE", "CODS", "COED", "COFF", "COFT", "COGS", "COHO", "COIF", "COIL", "COIN", "COIR", "COIT", "COKE", "COKY", "COLA", "COLD", "COLE", "COLL", "COLS", "COLT", "COLY", "COMA", "COMB", "COME", "COMM", "COMP", "COMS", "COND", "CONE", "CONF", "CONI", "CONK", "CONN", "CONS", "CONY", "COOF", "COOK", "COOL", "COOM", "COON", "COOP", "COOS", "COOT", "COPE", "COPS", "COPY", "CORD", "CORE", "CORF", "CORK", "CORM", "CORN", "CORS", "CORY", "COSE", "COSH", "COSS", "COST", "COSY", "COTE", "COTH", "COTS", "COTT", "COUP", "COUR", "COVE", "COWK", "COWL", "COWP", "COWS", "COWY", "COXA", "COXY", "COYS", "COZE", "COZY", "CRAB", "CRAG", "CRAM", "CRAN", "CRAP", "CRAW", "CRAY", "CRED", "CREE", "CREM", "CREW", "CRIB", "CRIM", "CRIS", "CRIT", "CROC", "CROG", "CROP", "CROW", "CRUD", "CRUE", "CRUS", "CRUX", "CUBE", "CUBS", "CUDS", "CUED", "CUES", "CUFF", "CUIF", "CUIT", "CUKE", "CULL", "CULM", "CULT", "CUNT", "CUPS", "CURB", "CURD", "CURE", "CURF", "CURL", "CURN", "CURR", "CURS", "CURT", "CUSH", "CUSK", "CUSP", "CUSS", "CUTE", "CUTS", "CWMS", "CYAN", "CYMA", "CYME", "CYST", "CYTE", "CZAR", "DABS", "DACE", "DACK", "DADA", "DADO", "DADS", "DAES", "DAFF", "DAFT", "DAGO", "DAGS", "DAHL", "DAHS", "DAIS", "DAKS", "DALE", "DALI", "DALS", "DALT", "DAME", "DAMN", "DAMP", "DAMS", "DANG", "DANK", "DANS", "DANT", "DAPS", "DARB", "DARE", "DARG", "DARI", "DARK", "DARN", "DART", "DASH", "DATA", "DATE", "DATO", "DAUB", "DAUD", "DAUR", "DAUT", "DAVY", "DAWD", "DAWK", "DAWN", "DAWS", "DAWT", "DAYS", "DAZE", "DEAD", "DEAF", "DEAL", "DEAN", "DEAR", "DEAW", "DEBE", "DEBS", "DEBT", "DECK", "DECO", "DEED", "DEEK", "DEEM", "DEEN", "DEEP", "DEER", "DEES", "DEET", "DEEV", "DEFI", "DEFT", "DEFY", "DEGS", "DEID", "DEIF", "DEIL", "DEKE", "DELE", "DELF", "DELI", "DELL", "DELO", "DELS", "DELT", "DEME", "DEMO", "DEMY", "DENE", "DENI",
            "DENS", "DENT", "DENY", "DERE", "DERM", "DERN", "DERO", "DERV", "DESI", "DESK", "DEUS", "DEVA", "DEVS", "DEWS", "DEWY", "DEXY", "DEYS", "DHAK", "DHAL", "DHOL", "DHOW", "DIAL", "DIBS", "DICE", "DICH", "DICK", "DICT", "DIDO", "DIDY", "DIEB", "DIED", "DIEL", "DIES", "DIET", "DIFF", "DIFS", "DIGS", "DIKA", "DIKE", "DILL", "DIME", "DIMP", "DIMS", "DINE", "DING", "DINK", "DINO", "DINS", "DINT", "DIOL", "DIPS", "DIPT", "DIRE", "DIRK", "DIRL", "DIRT", "DISA", "DISC", "DISH", "DISK", "DISS", "DITA", "DITE", "DITS", "DITT", "DITZ", "DIVA", "DIVE", "DIVI", "DIVS", "DIXI", "DIXY", "DJIN", "DOAB", "DOAT", "DOBS", "DOBY", "DOCK", "DOCO", "DOCS", "DODO", "DODS", "DOEK", "DOEN", "DOER", "DOES", "DOFF", "DOGE", "DOGS", "DOGY", "DOHS", "DOIT", "DOJO", "DOLE", "DOLL", "DOLS", "DOLT", "DOME", "DOMS", "DOMY", "DONA", "DONE", "DONG", "DONS", "DOOB", "DOOK", "DOOL", "DOOM", "DOON", "DOOR", "DOOS", "DOPA", "DOPE", "DOPS", "DOPY", "DORB", "DORE", "DORK", "DORM", "DORP", "DORR", "DORS", "DORT", "DORY", "DOSE", "DOSH", "DOSS", "DOST", "DOTE", "DOTH", "DOTS", "DOTY", "DOUC", "DOUK", "DOUM", "DOUN", "DOUP", "DOUR", "DOUT", "DOUX", "DOVE", "DOWD", "DOWF", "DOWL", "DOWN", "DOWP", "DOWS", "DOWT", "DOXY", "DOYS", "DOZE", "DOZY", "DRAB", "DRAC", "DRAD", "DRAG", "DRAM", "DRAP", "DRAT", "DRAW", "DRAY", "DREE", "DREG", "DREK", "DREW", "DREY", "DRIB", "DRIP", "DROP", "DROW", "DRUB", "DRUG", "DRUM", "DRYS", "DSOS", "DUAD", "DUAL", "DUAN", "DUAR", "DUBS", "DUCE", "DUCI", "DUCK", "DUCT", "DUDE", "DUDS", "DUED", "DUEL", "DUES", "DUET", "DUFF", "DUGS", "DUIT", "DUKA", "DUKE", "DULE", "DULL", "DULY", "DUMA", "DUMB", "DUMP", "DUNE", "DUNG", "DUNK", "DUNS", "DUNT", "DUOS", "DUPE", "DUPS", "DURA", "DURE", "DURN", "DURO", "DURR", "DUSH", "DUSK", "DUST", "DUTY", "DWAM", "DYAD", "DYED", "DYER", "DYES", "DYKE", "DYNE", "DZHO", "DZOS", "EACH", "EALE", "EANS", "EARD", "EARL", "EARN", "EARS", "EASE", "EAST", "EASY", "EATH", "EATS", "EAUS", "EAUX", "EAVE", "EBBS", "EBON", "ECAD", "ECCE", "ECCO", "ECHE", "ECHO", "ECHT", "ECOD", "ECOS", "ECRU", "ECUS", "EDDO", "EDDY", "EDGE", "EDGY", "EDHS", "EDIT", "EECH", "EELS", "EELY", "EERY", "EEVN", "EFFS", "EFTS", "EGAD", "EGAL", "EGER", "EGGS", "EGGY", "EGIS", "EGMA", "EGOS", "EHED", "EIDE", "EIKS", "EILD", "EINA", "EINE", "EISH", "EKED", "EKES", "EKKA", "ELAN", "ELDS", "ELFS", "ELHI", "ELKS", "ELLS", "ELMS", "ELMY", "ELSE", "ELTS", "EMES", "EMEU", "EMIC", "EMIR", "EMIT", "EMMA", "EMMY", "EMOS", "EMPT", "EMUS", "EMYD", "EMYS", "ENDS", "ENES", "ENEW", "ENGS", "ENOL", "ENOW", "ENUF", "ENVY", "EOAN", "EONS", "EORL", "EPEE", "EPHA", "EPIC", "EPOS", "ERAS", "ERED", "ERES", "EREV", "ERGO", "ERGS", "ERIC", "ERKS", "ERNE", "ERNS", "EROS", "ERRS", "ERST", "ERUV", "ESES", "ESKY", "ESNE", "ESPY", "ESSE", "ESTS", "ETAS", "ETAT", "ETCH", "ETEN", "ETHE", "ETHS", "ETIC", "ETNA", "ETUI", "EUGE", "EUGH", "EUKS", "EUOI", "EURO", "EVEN", "EVER", "EVES", "EVET", "EVIL", "EVOE", "EVOS", "EWER", "EWES", "EWKS", "EWTS", "EXAM", "EXEC", "EXED", "EXES", "EXIT", "EXON", "EXPO", "EXUL", "EYAS", "EYED", "EYEN", "EYER", "EYES", "EYNE", "EYOT", "EYRA", "EYRE", "EYRY", "FAAN", "FAAS", "FABS", "FACE", "FACT", "FADE", "FADO", "FADS", "FADY", "FAFF", "FAGS", "FAHS", "FAIK", "FAIL", "FAIN", "FAIR", "FAIX", "FAKE", "FALL", "FALX", "FAME", "FAND", "FANE", "FANG", "FANK", "FANO", "FANS", "FARD", "FARE", "FARL", "FARM", "FARO", "FARS", "FART", "FASH", "FAST", "FATE", "FATS", "FAUN", "FAUR", "FAUT", "FAUX", "FAVA", "FAVE", "FAWN", "FAWS", "FAYS", "FAZE", "FEAL", "FEAR", "FEAT", "FECK", "FEDS", "FEEB", "FEED", "FEEL", "FEEN", "FEER", "FEES", "FEET", "FEGS", "FEHM", "FEHS", "FEIS", "FELL", "FELT", "FEME", "FEMS", "FEND", "FENI", "FENS", "FENT", "FEOD", "FERE", "FERM", "FERN", "FESS", "FEST", "FETA", "FETE", "FETS", "FETT", "FEUD", "FEUS", "FEYS", "FIAR", "FIAT", "FIBS", "FICE", "FICO", "FIDO", "FIDS", "FIEF", "FIER", "FIFE", "FIFI", "FIGO", "FIGS", "FIKE", "FIKY", "FILA", "FILE", "FILL", "FILM", "FILO", "FILS", "FIND", "FINE", "FINI", "FINK", "FINO", "FINS", "FIRE", "FIRK", "FIRM", "FIRN", "FIRS", "FISC", "FISH", "FISK", "FIST", "FITS", "FITT", "FIVE", "FIXT", "FIZZ", "FLAB", "FLAG", "FLAK", "FLAM", "FLAN", "FLAP", "FLAT", "FLAW", "FLAX", "FLAY", "FLEA", "FLED", "FLEE", "FLEG", "FLEW", "FLEX", "FLEY", "FLIC", "FLIM", "FLIP", "FLIR", "FLIT", "FLIX", "FLOC", "FLOE", "FLOG", "FLOP", "FLOR", "FLOW", "FLUB", "FLUE", "FLUS", "FLUX", "FOAL", "FOAM", "FOBS", "FOCI", "FOEN", "FOES", "FOGS", "FOGY", "FOHN", "FOHS", "FOID", "FOIL", "FOIN", "FOLD", "FOLK", "FOND", "FONE", "FONS", "FONT", "FOOD", "FOOL", "FOOT", "FOPS", "FORA", "FORB", "FORD", "FORE", "FORK", "FORM", "FORT", "FOSS", "FOUD", "FOUL", "FOUR", "FOUS", "FOWL", "FOXY", "FOYS", "FOZY", "FRAB", "FRAE", "FRAG", "FRAP", "FRAS", "FRAT", "FRAU", "FRAY", "FREE", "FRET", "FRIB", "FRIG", "FRIS", "FRIT", "FRIZ", "FROE", "FROG", "FROM", "FROS", "FROW", "FRUG", "FUBS", "FUCI", "FUCK", "FUDS", "FUEL", "FUFF", "FUGS", "FUGU", "FUJI", "FULL", "FUME", "FUMS", "FUMY", "FUND", "FUNG", "FUNK", "FUNS", "FURL", "FURR", "FURS", "FURY", "FUSC", "FUSE", "FUSS", "FUST", "FUTZ", "FUZE", "FUZZ", "FYCE", "FYKE", "FYLE", "FYRD", "GABS", "GABY", "GADE", "GADI", "GADS", "GAED", "GAEN", "GAES", "GAFF", "GAGA", "GAGE", "GAGS", "GAID", "GAIN", "GAIR", "GAIT", "GAJO", "GALA", "GALE", "GALL", "GALS", "GAMA", "GAMB", "GAME", "GAMP", "GAMS", "GAMY", "GANE", "GANG", "GANS", "GANT", "GAOL", "GAPE", "GAPO", "GAPS", "GAPY", "GARB", "GARE", "GARI", "GARS", "GART", "GASH", "GASP", "GAST", "GATE", "GATH", "GATS", "GAUD", "GAUM", "GAUN", "GAUP", "GAUR", "GAUS", "GAVE", "GAWD", "GAWK", "GAWP", "GAYS", "GAZE", "GAZY", "GEAL", "GEAN", "GEAR", "GEAT", "GECK", "GEDS", "GEED", "GEEK", "GEEP", "GEES", "GEEZ", "GEIT", "GELD", "GELS", "GELT", "GEMS", "GENA", "GENE", "GENS", "GENT", "GENU", "GEOS", "GERE", "GERM", "GERT", "GEST", "GETA", "GETS", "GEUM", "GHAT", "GHEE", "GHIS", "GIBE", "GIBS", "GIDS", "GIED", "GIEN", "GIES", "GIFT", "GIGA", "GIGS", "GILA", "GILD", "GILL", "GILT", "GIMP", "GING", "GINK", "GINN", "GINS", "GIOS", "GIPS", "GIRD", "GIRL", "GIRN", "GIRO", "GIRR", "GIRT", "GISM", "GIST", "GITE", "GITS", "GIVE", "GIZZ", "GJUS", "GLAD", "GLAM", "GLED", "GLEE", "GLEG", "GLEI", "GLEN", "GLEY", "GLIA", "GLIB", "GLID", "GLIM", "GLIT", "GLOB", "GLOM", "GLOP", "GLOW", "GLUE", "GLUG", "GLUM", "GLUT", "GNAR", "GNAT", "GNAW", "GNOW", "GNUS", "GOAD", "GOAF", "GOAL", "GOAS", "GOAT", "GOBO", "GOBS", "GOBY", "GODS", "GOEL", "GOER", "GOES", "GOEY", "GOFF", "GOGO", "GOLD", "GOLE", "GOLF", "GOLP", "GONE", "GONG", "GONK", "GONS", "GOOD", "GOOF", "GOOG", "GOOK", "GOOL", "GOON", "GOOP", "GOOR", "GOOS", "GORA", "GORE", "GORI", "GORM", "GORP", "GORY", "GOSH", "GOSS", "GOTH", "GOUK", "GOUT", "GOVS", "GOWD", "GOWF", "GOWK", "GOWL", "GOWN", "GOYS", "GRAB", "GRAD", "GRAM", "GRAN", "GRAT", "GRAV", "GRAY", "GREE", "GREN", "GREW", "GREX", "GREY", "GRID", "GRIG", "GRIM", "GRIN", "GRIP", "GRIS", "GRIT", "GROG", "GROK", "GROT", "GROW", "GRUB", "GRUE", "GRUM", "GUAN", "GUAR", "GUBS", "GUCK", "GUDE", "GUES", "GUFF", "GUGA", "GUID", "GULA", "GULE", "GULF", "GULL", "GULP", "GULS", "GULY", "GUMP", "GUMS", "GUNG", "GUNK", "GUNS", "GUPS", "GURL", "GURN", "GURS", "GURU", "GUSH", "GUST", "GUTS", "GUVS", "GUYS", "GYAL", "GYBE", "GYMP", "GYMS", "GYNY", "GYPS", "GYRE", "GYRI", "GYRO", "GYTE", "GYVE", "HAAF", "HAAR", "HABU", "HACK", "HADE", "HADJ", "HADS", "HAED", "HAEM", "HAEN", "HAES", "HAET", "HAFF", "HAFT", "HAGG", "HAGS", "HAHA", "HAHS", "HAIK", "HAIL", "HAIN", "HAIR", "HAJI", "HAJJ", "HAKA", "HAKE", "HAKU", "HALE", "HALF", "HALL", "HALM", "HALO", "HALT", "HAME", "HAMS", "HAND", "HANG", "HANK", "HANT", "HAPS", "HAPU", "HARD", "HARE", "HARK", "HARL", "HARM", "HARN", "HARO", "HARP", "HART", "HASH", "HASK", "HASP", "HAST", "HATE", "HATH", "HATS", "HAUD", "HAUF", "HAUL", "HAUT", "HAVE", "HAWK", "HAWM", "HAWS", "HAYS", "HAZE", "HAZY", "HEAD", "HEAL", "HEAP", "HEAR", "HEAT", "HEBE", "HECH", "HECK", "HEED", "HEEL", "HEFT", "HEHS", "HEID", "HEIL", "HEIR", "HELD", "HELE", "HELL", "HELM", "HELO", "HELP", "HEME", "HEMP", "HEMS", "HEND", "HENS", "HENT",
            "HEPS", "HEPT", "HERB", "HERD", "HERE", "HERL", "HERM", "HERN", "HERO", "HERS", "HERY", "HESP", "HEST", "HETE", "HETH", "HETS", "HEWN", "HEWS", "HEYS", "HICK", "HIDE", "HIED", "HIES", "HIGH", "HIKE", "HILA", "HILD", "HILI", "HILL", "HILT", "HIMS", "HIND", "HING", "HINS", "HINT", "HIOI", "HIPS", "HIPT", "HIRE", "HISH", "HISN", "HISS", "HIST", "HITS", "HIVE", "HIYA", "HIZZ", "HOAR", "HOAS", "HOAX", "HOBO", "HOBS", "HOCK", "HODS", "HOED", "HOER", "HOES", "HOGG", "HOGH", "HOGS", "HOHA", "HOHS", "HOIK", "HOKA", "HOKE", "HOKI", "HOLD", "HOLE", "HOLK", "HOLM", "HOLP", "HOLS", "HOLT", "HOLY", "HOMA", "HOME", "HOMO", "HOMS", "HOMY", "HOND", "HONE", "HONG", "HONK", "HONS", "HOOD", "HOOF", "HOOK", "HOON", "HOOP", "HOOT", "HOPE", "HOPS", "HORA", "HORE", "HORI", "HORN", "HORS", "HOSE", "HOSS", "HOST", "HOTE", "HOTS", "HOUF", "HOUR", "HOUT", "HOVE", "HOWE", "HOWF", "HOWK", "HOWL", "HOWS", "HOYA", "HOYS", "HUBS", "HUCK", "HUED", "HUER", "HUES", "HUFF", "HUGE", "HUGS", "HUGY", "HUHU", "HUIA", "HUIC", "HUIS", "HULA", "HULE", "HULK", "HULL", "HUMA", "HUMF", "HUMP", "HUMS", "HUNG", "HUNH", "HUNK", "HUNS", "HUNT", "HUPS", "HURL", "HURT", "HUSH", "HUSK", "HUSO", "HUSS", "HUTS", "HWAN", "HWYL", "HYED", "HYEN", "HYES", "HYKE", "HYLA", "HYLE", "HYMN", "HYPE", "HYPO", "HYPS", "HYTE", "IAMB", "IBEX", "IBIS", "ICED", "ICER", "ICES", "ICHS", "ICKY", "ICON", "IDEA", "IDEE", "IDEM", "IDES", "IDLE", "IDLY", "IDOL", "IDYL", "IFFY", "IGAD", "IGGS", "IGLU", "IKAN", "IKAT", "IKON", "ILEA", "ILEX", "ILIA", "ILKA", "ILKS", "ILLS", "ILLY", "IMAM", "IMID", "IMMY", "IMPI", "IMPS", "INBY", "INCH", "INFO", "INGO", "INIA", "INKS", "INKY", "INLY", "INNS", "INRO", "INTI", "INTO", "IONS", "IOTA", "IRED", "IRES", "IRID", "IRIS", "IRKS", "IRON", "ISBA", "ISIT", "ISLE", "ISMS", "ISNA", "ISOS", "ITAS", "ITCH", "ITEM", "IURE", "IWIS", "IXIA", "IZAR", "JAAP", "JABS", "JACK", "JADE", "JAFA", "JAGA", "JAGG", "JAGS", "JAIL", "JAKE", "JAKS", "JAMB", "JAMS", "JANE", "JANN", "JAPE", "JAPS", "JARK", "JARL", "JARP", "JARS", "JASP", "JASS", "JASY", "JATO", "JAUK", "JAUP", "JAVA", "JAWS", "JAXY", "JAYS", "JAZY", "JAZZ", "JEAN", "JEAT", "JEDI", "JEED", "JEEL", "JEEP", "JEER", "JEES", "JEEZ", "JEFE", "JEFF", "JEHU", "JELL", "JEON", "JERK", "JESS", "JEST", "JETE", "JETS", "JEUX", "JEWS", "JIAO", "JIBB", "JIBE", "JIBS", "JIFF", "JIGS", "JILL", "JILT", "JIMP", "JINK", "JINN", "JINS", "JINX", "JIRD", "JISM", "JIVE", "JIVY", "JIZZ", "JOBE", "JOBS", "JOCK", "JOCO", "JOES", "JOEY", "JOGS", "JOHN", "JOIN", "JOKE", "JOKY", "JOLE", "JOLL", "JOLS", "JOLT", "JOMO", "JONG", "JOOK", "JORS", "JOSH", "JOSS", "JOTA", "JOTS", "JOUK", "JOUR", "JOWL", "JOWS", "JOYS", "JUBA", "JUBE", "JUCO", "JUDO", "JUDS", "JUDY", "JUGA", "JUGS", "JUJU", "JUKE", "JUKU", "JUMP", "JUNK", "JUPE", "JURA", "JURE", "JURY", "JUST", "JUTE", "JUTS", "JUVE", "JYNX", "KAAL", "KAAS", "KABS", "KADE", "KADI", "KAED", "KAES", "KAFS", "KAGO", "KAGU", "KAID", "KAIE", "KAIF", "KAIK", "KAIL", "KAIM", "KAIN", "KAIS", "KAKA", "KAKI", "KAKS", "KALE", "KALI", "KAMA", "KAME", "KAMI", "KANA", "KANE", "KANG", "KANS", "KANT", "KAON", "KAPA", "KAPH", "KARA", "KARK", "KARN", "KARO", "KART", "KATA", "KATI", "KATS", "KAVA", "KAWA", "KAWS", "KAYO", "KAYS", "KAZI", "KBAR", "KEAS", "KEBS", "KECK", "KEDS", "KEEF", "KEEK", "KEEL", "KEEN", "KEEP", "KEET", "KEFS", "KEGS", "KEIR", "KEKS", "KELL", "KELP", "KELT", "KEMB", "KEMP", "KENO", "KENS", "KENT", "KEPI", "KEPS", "KEPT", "KERB", "KERF", "KERN", "KERO", "KESH", "KEST", "KETA", "KETE", "KETO", "KETS", "KEWL", "KEYS", "KHAF", "KHAN", "KHAT", "KHET", "KHIS", "KHOR", "KHUD", "KIBE", "KICK", "KIDS", "KIEF", "KIER", "KIFF", "KIFS", "KIKE", "KILD", "KILL", "KILN", "KILO", "KILP", "KILT", "KINA", "KIND", "KINE", "KING", "KINK", "KINO", "KINS", "KIPE", "KIPP", "KIPS", "KIRK", "KIRN", "KIRS", "KISH", "KISS", "KIST", "KITE", "KITH", "KITS", "KIVA", "KIWI", "KLAP", "KLIK", "KNAG", "KNAP", "KNAR", "KNEE", "KNEW", "KNIT", "KNOB", "KNOP", "KNOT", "KNOW", "KNUB", "KNUR", "KNUT", "KOAN", "KOAP", "KOAS", "KOBO", "KOBS", "KOEL", "KOFF", "KOHA", "KOHL", "KOIS", "KOJI", "KOLA", "KOLO", "KOND", "KONK", "KONS", "KOOK", "KOPH", "KOPS", "KORA", "KORE", "KORO", "KORS", "KORU", "KOSS", "KOTO", "KOWS", "KRAB", "KRIS", "KSAR", "KUDO", "KUDU", "KUEH", "KUES", "KUFI", "KUIA", "KUKU", "KULA", "KUNA", "KUNE", "KURI", "KURU", "KUTA", "KUTI", "KUTU", "KUZU", "KVAS", "KYAK", "KYAR", "KYAT", "KYBO", "KYES", "KYLE", "KYND", "KYNE", "KYPE", "KYTE", "KYUS", "LABS", "LACE", "LACK", "LACS", "LACY", "LADE", "LADS", "LADY", "LAER", "LAGS", "LAHS", "LAIC", "LAID", "LAIK", "LAIN", "LAIR", "LAKE", "LAKH", "LAKY", "LALL", "LAMA", "LAMB", "LAME", "LAMP", "LAMS", "LANA", "LAND", "LANE", "LANG", "LANK", "LANT", "LANX", "LAPS", "LARD", "LARE", "LARI", "LARK", "LARN", "LARS", "LASE", "LASH", "LASS", "LAST", "LATE", "LATH", "LATI", "LATS", "LATU", "LAUD", "LAUF", "LAVA", "LAVE", "LAVS", "LAWK", "LAWN", "LAWS", "LAYS", "LAZE", "LAZO", "LAZY", "LEAD", "LEAF", "LEAK", "LEAL", "LEAM", "LEAN", "LEAP", "LEAR", "LEAS", "LEAT", "LECH", "LEED", "LEEK", "LEEP", "LEER", "LEES", "LEET", "LEFT", "LEGS", "LEHR", "LEIR", "LEIS", "LEKE", "LEKS", "LEKU", "LEME", "LEND", "LENG", "LENO", "LENS", "LENT", "LEPS", "LEPT", "LERE", "LERP", "LESS", "LEST", "LETS", "LEUD", "LEVA", "LEVE", "LEVO", "LEVY", "LEWD", "LEYS", "LEZZ", "LIAR", "LIAS", "LIBS", "LICE", "LICH", "LICK", "LIDO", "LIDS", "LIED", "LIEF", "LIEN", "LIER", "LIES", "LIEU", "LIFE", "LIFT", "LIGS", "LIKE", "LILL", "LILO", "LILT", "LILY", "LIMA", "LIMB", "LIME", "LIMN", "LIMO", "LIMP", "LIMY", "LIND", "LINE", "LING", "LINK", "LINN", "LINO", "LINS", "LINT", "LINY", "LION", "LIPA", "LIPE", "LIPO", "LIPS", "LIRA", "LIRE", "LIRI", "LIRK", "LISK", "LISP", "LIST", "LITE", "LITH", "LITS", "LITU", "LIVE", "LOAD", "LOAF", "LOAM", "LOAN", "LOBE", "LOBI", "LOBO", "LOBS", "LOCA", "LOCH", "LOCI", "LOCK", "LOCO", "LODE", "LODS", "LOFT", "LOGE", "LOGO", "LOGS", "LOGY", "LOID", "LOIN", "LOIR", "LOKE", "LOLL", "LOMA", "LOME", "LONE", "LONG", "LOOF", "LOOK", "LOOM", "LOON", "LOOP", "LOOR", "LOOS", "LOOT", "LOPE", "LOPS", "LORD", "LORE", "LORN", "LORY", "LOSE", "LOSH", "LOSS", "LOST", "LOTA", "LOTE", "LOTH", "LOTI", "LOTO", "LOTS", "LOUD", "LOUN", "LOUP", "LOUR", "LOUS", "LOUT", "LOVE", "LOWE", "LOWN", "LOWP", "LOWS", "LOWT", "LOYS", "LUAU", "LUBE", "LUCE", "LUCK", "LUDE", "LUDO", "LUDS", "LUES", "LUFF", "LUGE", "LUGS", "LUIT", "LUKE", "LULL", "LULU", "LUMA", "LUMP", "LUMS", "LUNA", "LUNE", "LUNG", "LUNK", "LUNT", "LUNY", "LURE", "LURK", "LURS", "LUSH", "LUSK", "LUST", "LUTE", "LUTZ", "LUVS", "LUXE", "LWEI", "LYAM", "LYCH", "LYES", "LYME", "LYMS", "LYNE", "LYNX", "LYRA", "LYRE", "LYSE", "LYTE", "MAAR", "MAAS", "MABE", "MACE", "MACH", "MACK", "MACS", "MADE", "MADS", "MAES", "MAGE", "MAGG", "MAGI", "MAGS", "MAID", "MAIK", "MAIL", "MAIM", "MAIN", "MAIR", "MAKE", "MAKI", "MAKO", "MAKS", "MALA", "MALE", "MALI", "MALL", "MALM", "MALS", "MALT", "MAMA", "MAMS", "MANA", "MAND", "MANE", "MANG", "MANI", "MANO", "MANS", "MANY", "MAPS", "MARA", "MARC", "MARD", "MARE", "MARG", "MARK", "MARL", "MARM", "MARS", "MART", "MARY", "MASA", "MASE", "MASH", "MASK", "MASS", "MAST", "MASU", "MATE", "MATH", "MATS", "MATT", "MATY", "MAUD", "MAUL", "MAUN", "MAUT", "MAWK", "MAWN", "MAWR", "MAWS", "MAXI", "MAYA", "MAYO", "MAYS", "MAZE", "MAZY", "MEAD", "MEAL", "MEAN", "MEAT", "MECK", "MEDS", "MEED", "MEEK", "MEER", "MEES", "MEET", "MEFF", "MEGA", "MEGS", "MEIN", "MELA", "MELD", "MELL", "MELS", "MELT", "MEME", "MEMO", "MEMS", "MEND", "MENE", "MENG", "MENO", "MENT", "MENU", "MEOU", "MEOW", "MERC", "MERE", "MERI", "MERK", "MERL", "MESA", "MESE", "MESH", "MESS", "META", "METE", "METH", "METS", "MEUS", "MEVE", "MEWL", "MEWS", "MEZE", "MEZZ", "MHOS", "MIBS", "MICA", "MICE", "MICH", "MICK", "MICO", "MICS", "MIDI", "MIDS", "MIEN", "MIFF", "MIGG", "MIGS", "MIHA", "MIHI", "MIKE", "MILD", "MILE", "MILK", "MILL", "MILO", "MILS", "MILT", "MIME", "MINA", "MIND", "MINE", "MING", "MINI", "MINK", "MINO", "MINT", "MINX", "MINY", "MIPS", "MIRE", "MIRI", "MIRK", "MIRO", "MIRS", "MIRV", "MIRY", "MISE", "MISO", "MISS",
            "MIST", "MITE", "MITT", "MITY", "MIXT", "MIXY", "MIZZ", "MNAS", "MOAI", "MOAN", "MOAS", "MOAT", "MOBE", "MOBS", "MOBY", "MOCH", "MOCK", "MOCS", "MODE", "MODI", "MODS", "MOER", "MOES", "MOFO", "MOGS", "MOHR", "MOIL", "MOIT", "MOJO", "MOKE", "MOKI", "MOKO", "MOLA", "MOLD", "MOLE", "MOLL", "MOLS", "MOLT", "MOLY", "MOME", "MOMI", "MOMS", "MONA", "MONG", "MONK", "MONO", "MONS", "MONY", "MOOD", "MOOI", "MOOK", "MOOL", "MOON", "MOOP", "MOOR", "MOOS", "MOOT", "MOPE", "MOPS", "MOPY", "MORA", "MORE", "MORN", "MORS", "MORT", "MOSE", "MOSH", "MOSK", "MOSS", "MOST", "MOTE", "MOTH", "MOTI", "MOTS", "MOTT", "MOTU", "MOUE", "MOUP", "MOUS", "MOVE", "MOWA", "MOWN", "MOWS", "MOXA", "MOYA", "MOYL", "MOYS", "MOZE", "MOZO", "MOZZ", "MUCH", "MUCK", "MUDS", "MUFF", "MUGG", "MUGS", "MUID", "MUIL", "MUIR", "MULE", "MULL", "MUMM", "MUMP", "MUMS", "MUMU", "MUNG", "MUNI", "MUNS", "MUNT", "MUON", "MURA", "MURE", "MURK", "MURL", "MURR", "MUSE", "MUSH", "MUSK", "MUSO", "MUSS", "MUST", "MUTE", "MUTI", "MUTS", "MUTT", "MUZZ", "MYAL", "MYCS", "MYNA", "MYTH", "MYXO", "MZEE", "NAAM", "NAAN", "NABE", "NABK", "NABS", "NACH", "NADA", "NADS", "NAFF", "NAGA", "NAGS", "NAIF", "NAIK", "NAIL", "NAIN", "NALA", "NAME", "NAMS", "NAMU", "NANA", "NANE", "NANS", "NAOI", "NAOS", "NAPA", "NAPE", "NAPS", "NARC", "NARD", "NARE", "NARK", "NARY", "NATS", "NAVE", "NAVY", "NAYS", "NAZE", "NAZI", "NEAL", "NEAP", "NEAR", "NEAT", "NEBS", "NECK", "NEDS", "NEED", "NEEM", "NEEP", "NEFS", "NEGS", "NEIF", "NEKS", "NEMA", "NEMN", "NENE", "NEON", "NEPS", "NERD", "NERK", "NESH", "NESS", "NEST", "NETE", "NETS", "NETT", "NEUK", "NEUM", "NEVE", "NEVI", "NEWS", "NEWT", "NEXT", "NIBS", "NICE", "NICK", "NIDE", "NIDI", "NIDS", "NIED", "NIEF", "NIES", "NIFE", "NIFF", "NIGH", "NILL", "NILS", "NIMB", "NIMS", "NINE", "NIPA", "NIPS", "NIRL", "NISH", "NISI", "NITE", "NITS", "NIXE", "NIXY", "NOAH", "NOBS", "NOCK", "NODE", "NODI", "NODS", "NOEL", "NOES", "NOGG", "NOGS", "NOIL", "NOIR", "NOLE", "NOLL", "NOLO", "NOMA", "NOME", "NOMS", "NONA", "NONE", "NONG", "NONI", "NOOK", "NOON", "NOOP", "NOPE", "NORI", "NORK", "NORM", "NOSE", "NOSH", "NOSY", "NOTA", "NOTE", "NOTT", "NOUL", "NOUN", "NOUP", "NOUS", "NOUT", "NOVA", "NOWL", "NOWN", "NOWS", "NOWT", "NOWY", "NOYS", "NUBS", "NUDE", "NUFF", "NUKE", "NULL", "NUMB", "NUNS", "NURD", "NURL", "NURR", "NURS", "NUTS", "NYAS", "NYED", "NYES", "OAFS", "OAKS", "OAKY", "OARS", "OARY", "OAST", "OATH", "OATS", "OBAS", "OBES", "OBEY", "OBIA", "OBIS", "OBIT", "OBOE", "OBOL", "OBOS", "OCAS", "OCCY", "OCHE", "OCTA", "ODAH", "ODAL", "ODAS", "ODDS", "ODEA", "ODES", "ODIC", "ODOR", "ODSO", "ODYL", "OFAY", "OFFS", "OGAM", "OGEE", "OGLE", "OGRE", "OHED", "OHIA", "OHMS", "OHOS", "OIKS", "OILS", "OILY", "OINK", "OINT", "OKAS", "OKAY", "OKEH", "OKES", "OKRA", "OKTA", "OLDS", "OLDY", "OLEA", "OLEO", "OLES", "OLID", "OLIO", "OLLA", "OLMS", "OLPE", "OMBU", "OMEN", "OMER", "OMIT", "OMOV", "ONCE", "ONER", "ONES", "ONIE", "ONLY", "ONOS", "ONST", "ONTO", "ONUS", "ONYX", "OOFS", "OOFY", "OOHS", "OOMS", "OONS", "OONT", "OOPS", "OOSE", "OOSY", "OOTS", "OOZE", "OOZY", "OPAH", "OPAL", "OPED", "OPEN", "OPES", "OPPO", "OPTS", "OPUS", "ORAD", "ORAL", "ORBS", "ORBY", "ORCA", "ORCS", "ORDO", "ORDS", "ORES", "ORFE", "ORFS", "ORGY", "ORLE", "ORRA", "ORTS", "ORYX", "ORZO", "OSAR", "OSES", "OSSA", "OTIC", "OTTO", "OUCH", "OUDS", "OUKS", "OULD", "OULK", "OUMA", "OUPA", "OUPH", "OUPS", "OURN", "OURS", "OUST", "OUTS", "OUZO", "OVAL", "OVEL", "OVEN", "OVER", "OVUM", "OWED", "OWER", "OWES", "OWLS", "OWLY", "OWNS", "OWRE", "OWSE", "OWTS", "OXEN", "OXER", "OXES", "OXID", "OXIM", "OYER", "OYES", "OYEZ", "PAAL", "PACA", "PACE", "PACK", "PACO", "PACS", "PACT", "PACY", "PADI", "PADS", "PAGE", "PAHS", "PAID", "PAIK", "PAIL", "PAIN", "PAIR", "PAIS", "PALE", "PALL", "PALM", "PALP", "PALS", "PALY", "PAMS", "PAND", "PANE", "PANG", "PANS", "PANT", "PAPA", "PAPE", "PAPS", "PARA", "PARD", "PARE", "PARK", "PARP", "PARR", "PARS", "PART", "PASE", "PASH", "PASS", "PAST", "PATE", "PATH", "PATS", "PATU", "PATY", "PAUA", "PAUL", "PAVE", "PAVS", "PAWA", "PAWK", "PAWL", "PAWN", "PAWS", "PAYS", "PEAG", "PEAK", "PEAL", "PEAN", "PEAR", "PEAS", "PEAT", "PEBA", "PECH", "PECK", "PECS", "PEDS", "PEED", "PEEK", "PEEL", "PEEN", "PEEP", "PEER", "PEES", "PEGH", "PEGS", "PEHS", "PEIN", "PEKE", "PELA", "PELE", "PELF", "PELL", "PELT", "PEND", "PENE", "PENI", "PENK", "PENS", "PENT", "PEON", "PEPO", "PEPS", "PERE", "PERI", "PERK", "PERM", "PERN", "PERP", "PERT", "PERV", "PESO", "PEST", "PETS", "PEWS", "PFFT", "PFUI", "PHAT", "PHEW", "PHIS", "PHIZ", "PHOH", "PHON", "PHOS", "PHOT", "PHUT", "PIAL", "PIAN", "PIAS", "PICA", "PICE", "PICK", "PICS", "PIED", "PIER", "PIES", "PIET", "PIGS", "PIKA", "PIKE", "PIKI", "PILA", "PILE", "PILI", "PILL", "PILY", "PIMA", "PIMP", "PINA", "PINE", "PING", "PINK", "PINS", "PINT", "PINY", "PION", "PIOY", "PIPA", "PIPE", "PIPI", "PIPS", "PIPY", "PIRL", "PIRN", "PIRS", "PISE", "PISH", "PISO", "PISS", "PITA", "PITH", "PITS", "PITY", "PIUM", "PIXY", "PIZE", "PLAN", "PLAP", "PLAT", "PLAY", "PLEA", "PLEB", "PLED", "PLEW", "PLEX", "PLIE", "PLIM", "PLOD", "PLOP", "PLOT", "PLOW", "PLOY", "PLUE", "PLUG", "PLUM", "PLUS", "POAS", "POCK", "POCO", "PODS", "POEM", "POEP", "POET", "POGO", "POGY", "POIS", "POKE", "POKY", "POLE", "POLK", "POLL", "POLO", "POLS", "POLT", "POLY", "POME", "POMO", "POMP", "POMS", "POND", "PONE", "PONG", "PONK", "PONS", "PONT", "PONY", "POOD", "POOF", "POOH", "POOK", "POOL", "POON", "POOP", "POOR", "POOS", "POOT", "POPE", "POPS", "PORE", "PORK", "PORN", "PORT", "PORY", "POSE", "POSH", "POSS", "POST", "POSY", "POTE", "POTS", "POTT", "POUF", "POUK", "POUR", "POUT", "POWN", "POWS", "POXY", "POZZ", "PRAD", "PRAM", "PRAO", "PRAT", "PRAU", "PRAY", "PREE", "PREM", "PREP", "PREX", "PREY", "PREZ", "PRIG", "PRIM", "PROA", "PROB", "PROD", "PROF", "PROG", "PROM", "PROO", "PROP", "PROS", "PROW", "PRUH", "PRYS", "PSIS", "PSST", "PTUI", "PUBE", "PUBS", "PUCE", "PUCK", "PUDS", "PUDU", "PUER", "PUFF", "PUGH", "PUGS", "PUHA", "PUIR", "PUJA", "PUKA", "PUKE", "PUKU", "PULA", "PULE", "PULI", "PULK", "PULL", "PULP", "PULS", "PULU", "PULY", "PUMA", "PUMP", "PUMY", "PUNA", "PUNG", "PUNK", "PUNS", "PUNT", "PUNY", "PUPA", "PUPS", "PUPU", "PURE", "PURI", "PURL", "PURR", "PURS", "PUSH", "PUSS", "PUTS", "PUTT", "PUTZ", "PUYS", "PYAS", "PYAT", "PYES", "PYET", "PYIC", "PYIN", "PYNE", "PYOT", "PYRE", "PYRO", "QADI", "QAID", "QATS", "QOPH", "QUAD", "QUAG", "QUAI", "QUAT", "QUAY", "QUEP", "QUEY", "QUID", "QUIM", "QUIN", "QUIP", "QUIT", "QUIZ", "QUOD", "QUOP", "RABI", "RACA", "RACE", "RACH", "RACK", "RACY", "RADE", "RADS", "RAFF", "RAFT", "RAGA", "RAGE", "RAGG", "RAGI", "RAGS", "RAHS", "RAIA", "RAID", "RAIK", "RAIL", "RAIN", "RAIS", "RAIT", "RAJA", "RAKE", "RAKI", "RAKU", "RALE", "RAMI", "RAMP", "RAMS", "RANA", "RAND", "RANG", "RANI", "RANK", "RANT", "RAPE", "RAPS", "RAPT", "RARE", "RARK", "RASE", "RASH", "RASP", "RAST", "RATA", "RATE", "RATH", "RATO", "RATS", "RATU", "RAUN", "RAVE", "RAWN", "RAWS", "RAYA", "RAYS", "RAZE", "RAZZ", "READ", "REAK", "REAL", "REAM", "REAN", "REAP", "REAR", "REBS", "RECK", "RECS", "REDD", "REDE", "REDO", "REDS", "REED", "REEF", "REEK", "REEL", "REEN", "REES", "REFS", "REFT", "REGO", "REGS", "REHS", "REIF", "REIK", "REIN", "REIS", "REKE", "RELY", "REMS", "REND", "RENK", "RENS", "RENT", "RENY", "REOS", "REPO", "REPP", "REPS", "RESH", "REST", "RETE", "RETS", "REVS", "REWS", "RHEA", "RHOS", "RHUS", "RIAL", "RIAS", "RIBA", "RIBS", "RICE", "RICH", "RICK", "RICY", "RIDE", "RIDS", "RIEL", "RIEM", "RIFE", "RIFF", "RIFS", "RIFT", "RIGG", "RIGS", "RILE", "RILL", "RIMA", "RIME", "RIMS", "RIMU", "RIMY", "RIND", "RINE", "RING", "RINK", "RINS", "RIOT", "RIPE", "RIPP", "RIPS", "RIPT", "RISE", "RISK", "RISP", "RITE", "RITS", "RITT", "RITZ", "RIVA", "RIVE", "RIVO", "RIZA", "ROAD", "ROAM", "ROAN", "ROAR", "ROBE", "ROBS", "ROCH", "ROCK", "ROCS", "RODE", "RODS", "ROED", "ROES", "ROIL", "ROIN", "ROJI", "ROKE", "ROKS", "ROKY", "ROLE", "ROLF", "ROLL", "ROMA", "ROMP", "ROMS", "RONE", "RONG", "RONT", "ROOD", "ROOF", "ROOK", "ROOM", "ROON", "ROOP", "ROOS", "ROOT", "ROPE",
            "ROPY", "RORE", "RORT", "RORY", "ROSE", "ROST", "ROSY", "ROTA", "ROTE", "ROTI", "ROTL", "ROTO", "ROTS", "ROUE", "ROUL", "ROUM", "ROUP", "ROUT", "ROUX", "ROVE", "ROWS", "ROWT", "RUBE", "RUBS", "RUBY", "RUCK", "RUCS", "RUDD", "RUDE", "RUDS", "RUED", "RUER", "RUES", "RUFF", "RUGA", "RUGS", "RUIN", "RUKH", "RULE", "RULY", "RUME", "RUMP", "RUMS", "RUND", "RUNE", "RUNG", "RUNS", "RUNT", "RURP", "RURU", "RUSA", "RUSE", "RUSH", "RUSK", "RUST", "RUTH", "RUTS", "RYAL", "RYAS", "RYES", "RYFE", "RYKE", "RYND", "RYOT", "RYPE", "SABE", "SABS", "SACK", "SACS", "SADE", "SADI", "SADO", "SADS", "SAFE", "SAFT", "SAGA", "SAGE", "SAGO", "SAGS", "SAGY", "SAIC", "SAID", "SAIL", "SAIM", "SAIN", "SAIR", "SAIS", "SAKE", "SAKI", "SALE", "SALL", "SALP", "SALS", "SALT", "SAMA", "SAME", "SAMP", "SAMS", "SAND", "SANE", "SANG", "SANK", "SANS", "SANT", "SAPS", "SARD", "SARI", "SARK", "SARS", "SASH", "SASS", "SATE", "SATI", "SAUL", "SAUT", "SAVE", "SAVS", "SAWN", "SAWS", "SAXE", "SAYS", "SCAB", "SCAD", "SCAG", "SCAM", "SCAN", "SCAR", "SCAT", "SCAW", "SCOG", "SCOP", "SCOT", "SCOW", "SCRY", "SCUD", "SCUG", "SCUL", "SCUM", "SCUP", "SCUR", "SCUT", "SCYE", "SEAL", "SEAM", "SEAN", "SEAR", "SEAS", "SEAT", "SECH", "SECO", "SECS", "SECT", "SEED", "SEEK", "SEEL", "SEEM", "SEEN", "SEEP", "SEER", "SEES", "SEGO", "SEGS", "SEIF", "SEIK", "SEIL", "SEIR", "SEIS", "SEKT", "SELD", "SELE", "SELF", "SELL", "SELS", "SEME", "SEMI", "SENA", "SEND", "SENE", "SENS", "SENT", "SEPS", "SEPT", "SERA", "SERE", "SERF", "SERK", "SERR", "SERS", "SESE", "SESH", "SESS", "SETA", "SETS", "SETT", "SEWN", "SEWS", "SEXT", "SEXY", "SEYS", "SHAD", "SHAG", "SHAH", "SHAM", "SHAN", "SHAT", "SHAW", "SHAY", "SHEA", "SHED", "SHES", "SHET", "SHEW", "SHIM", "SHIN", "SHIP", "SHIR", "SHIT", "SHIV", "SHMO", "SHOD", "SHOE", "SHOG", "SHOO", "SHOP", "SHOT", "SHOW", "SHRI", "SHUL", "SHUN", "SHUT", "SHWA", "SIAL", "SIBB", "SIBS", "SICE", "SICH", "SICK", "SICS", "SIDA", "SIDE", "SIDH", "SIEN", "SIES", "SIFT", "SIGH", "SIGN", "SIJO", "SIKA", "SIKE", "SILD", "SILE", "SILK", "SILL", "SILO", "SILT", "SIMA", "SIMI", "SIMP", "SIMS", "SIND", "SINE", "SING", "SINH", "SINK", "SINS", "SIPE", "SIPS", "SIRE", "SIRI", "SIRS", "SISS", "SIST", "SITE", "SITH", "SITS", "SITZ", "SIZE", "SIZY", "SJOE", "SKAG", "SKAS", "SKAT", "SKAW", "SKEE", "SKEG", "SKEN", "SKEO", "SKEP", "SKER", "SKET", "SKEW", "SKID", "SKIM", "SKIN", "SKIO", "SKIP", "SKIS", "SKIT", "SKOL", "SKRY", "SKUA", "SKUG", "SKYF", "SKYR", "SLAB", "SLAE", "SLAG", "SLAM", "SLAP", "SLAT", "SLAW", "SLAY", "SLED", "SLEE", "SLEW", "SLEY", "SLID", "SLIM", "SLIP", "SLIT", "SLOB", "SLOE", "SLOG", "SLOP", "SLOT", "SLOW", "SLUB", "SLUE", "SLUG", "SLUM", "SLUR", "SLUT", "SMEE", "SMEW", "SMIR", "SMIT", "SMOG", "SMUG", "SMUR", "SMUT", "SNAB", "SNAG", "SNAP", "SNAR", "SNAW", "SNEB", "SNED", "SNEE", "SNIB", "SNIG", "SNIP", "SNIT", "SNOB", "SNOD", "SNOG", "SNOT", "SNOW", "SNUB", "SNUG", "SNYE", "SOAK", "SOAP", "SOAR", "SOBA", "SOBS", "SOCA", "SOCK", "SOCS", "SODA", "SODS", "SOFA", "SOFT", "SOGS", "SOHO", "SOHS", "SOIL", "SOJA", "SOKE", "SOLA", "SOLD", "SOLE", "SOLI", "SOLO", "SOLS", "SOMA", "SOME", "SOMS", "SOMY", "SONE", "SONG", "SONS", "SOOK", "SOOL", "SOOM", "SOON", "SOOP", "SOOT", "SOPH", "SOPS", "SORA", "SORB", "SORD", "SORE", "SORI", "SORN", "SORT", "SOSS", "SOTH", "SOTS", "SOUK", "SOUL", "SOUM", "SOUP", "SOUR", "SOUS", "SOUT", "SOVS", "SOWF", "SOWL", "SOWM", "SOWN", "SOWP", "SOWS", "SOYA", "SOYS", "SPAE", "SPAG", "SPAM", "SPAN", "SPAR", "SPAS", "SPAT", "SPAW", "SPAY", "SPAZ", "SPEC", "SPED", "SPEK", "SPET", "SPEW", "SPIC", "SPIE", "SPIF", "SPIK", "SPIM", "SPIN", "SPIT", "SPIV", "SPOD", "SPOT", "SPRY", "SPUD", "SPUE", "SPUG", "SPUN", "SPUR", "SRIS", "STAB", "STAG", "STAP", "STAR", "STAT", "STAW", "STAY", "STED", "STEM", "STEN", "STEP", "STET", "STEW", "STEY", "STIE", "STIM", "STIR", "STOA", "STOB", "STOP", "STOT", "STOW", "STUB", "STUD", "STUM", "STUN", "STYE", "SUBA", "SUBS", "SUCH", "SUCK", "SUDD", "SUDS", "SUED", "SUER", "SUES", "SUET", "SUGH", "SUID", "SUIT", "SUKH", "SUKS", "SULK", "SULU", "SUMO", "SUMP", "SUMS", "SUMY", "SUNG", "SUNK", "SUNN", "SUNS", "SUPE", "SUPS", "SUQS", "SURA", "SURD", "SURE", "SURF", "SUSS", "SUSU", "SWAB", "SWAD", "SWAG", "SWAM", "SWAN", "SWAP", "SWAT", "SWAY", "SWEE", "SWEY", "SWIG", "SWIM", "SWIZ", "SWOB", "SWOP", "SWOT", "SWUM", "SYBO", "SYCE", "SYED", "SYEN", "SYES", "SYKE", "SYLI", "SYNC", "SYND", "SYNE", "SYPE", "SYPH", "TAAL", "TABI", "TABS", "TABU", "TACE", "TACH", "TACK", "TACO", "TACT", "TADS", "TAED", "TAEL", "TAES", "TAGS", "TAHA", "TAHR", "TAIG", "TAIL", "TAIN", "TAIS", "TAIT", "TAKA", "TAKE", "TAKI", "TAKS", "TAKY", "TALA", "TALC", "TALE", "TALI", "TALK", "TALL", "TAME", "TAMP", "TAMS", "TANA", "TANE", "TANG", "TANH", "TANK", "TANS", "TAOS", "TAPA", "TAPE", "TAPS", "TAPU", "TARA", "TARE", "TARN", "TARO", "TARP", "TARS", "TART", "TASH", "TASK", "TASS", "TATE", "TATH", "TATS", "TATT", "TATU", "TAUS", "TAUT", "TAVA", "TAVS", "TAWA", "TAWS", "TAWT", "TAXA", "TAXI", "TAYS", "TEAD", "TEAK", "TEAL", "TEAM", "TEAR", "TEAS", "TEAT", "TECH", "TECS", "TEDS", "TEDY", "TEED", "TEEK", "TEEL", "TEEM", "TEEN", "TEER", "TEES", "TEFF", "TEFS", "TEGG", "TEGS", "TEGU", "TEHR", "TEIL", "TELA", "TELD", "TELE", "TELL", "TELS", "TELT", "TEME", "TEMP", "TEMS", "TEND", "TENE", "TENS", "TENT", "TEPA", "TERF", "TERM", "TERN", "TEST", "TETE", "TETH", "TETS", "TEWS", "TEXT", "THAE", "THAN", "THAR", "THAT", "THAW", "THEE", "THEM", "THEN", "THEW", "THEY", "THIG", "THIN", "THIO", "THIR", "THIS", "THON", "THOU", "THRO", "THRU", "THUD", "THUG", "THUS", "TIAR", "TICE", "TICH", "TICK", "TICS", "TIDE", "TIDS", "TIDY", "TIED", "TIER", "TIES", "TIFF", "TIFT", "TIGE", "TIGS", "TIKA", "TIKE", "TIKI", "TILE", "TILL", "TILS", "TILT", "TIME", "TIND", "TINE", "TING", "TINK", "TINS", "TINT", "TINY", "TIPI", "TIPS", "TIPT", "TIRE", "TIRL", "TIRO", "TIRR", "TITE", "TITI", "TITS", "TIVY", "TIZZ", "TOAD", "TOBY", "TOCK", "TOCO", "TOCS", "TODS", "TODY", "TOEA", "TOED", "TOES", "TOEY", "TOFF", "TOFT", "TOFU", "TOGA", "TOGE", "TOGS", "TOHO", "TOIL", "TOIT", "TOKE", "TOKO", "TOLA", "TOLD", "TOLE", "TOLL", "TOLT", "TOLU", "TOMB", "TOME", "TOMO", "TOMS", "TONE", "TONG", "TONK", "TONS", "TONY", "TOOK", "TOOL", "TOOM", "TOON", "TOOT", "TOPE", "TOPH", "TOPI", "TOPO", "TOPS", "TORA", "TORC", "TORE", "TORI", "TORN", "TORO", "TORR", "TORS", "TORT", "TORY", "TOSA", "TOSE", "TOSH", "TOSS", "TOST", "TOTE", "TOTS", "TOUK", "TOUN", "TOUR", "TOUT", "TOWN", "TOWS", "TOWT", "TOWY", "TOYO", "TOYS", "TOZE", "TRAD", "TRAM", "TRAP", "TRAT", "TRAY", "TREE", "TREF", "TREK", "TRES", "TRET", "TREW", "TREY", "TREZ", "TRIE", "TRIG", "TRIM", "TRIN", "TRIO", "TRIP", "TROD", "TROG", "TRON", "TROP", "TROT", "TROW", "TROY", "TRUE", "TRUG", "TRYE", "TRYP", "TSAR", "TSKS", "TUAN", "TUBA", "TUBE", "TUBS", "TUCK", "TUFA", "TUFF", "TUFT", "TUGS", "TUIS", "TULE", "TUMP", "TUMS", "TUNA", "TUND", "TUNE", "TUNG", "TUNS", "TUNY", "TUPS", "TURD", "TURF", "TURK", "TURM", "TURN", "TUSH", "TUSK", "TUTS", "TUTU", "TUZZ", "TWAE", "TWAL", "TWAS", "TWAT", "TWAY", "TWEE", "TWIG", "TWIN", "TWIT", "TWOS", "TYDE", "TYED", "TYEE", "TYER", "TYES", "TYGS", "TYIN", "TYKE", "TYMP", "TYND", "TYNE", "TYPE", "TYPO", "TYPP", "TYPY", "TYRE", "TYRO", "TYTE", "TZAR", "UDAL", "UDON", "UDOS", "UEYS", "UFOS", "UGHS", "UGLY", "UKES", "ULAN", "ULES", "ULEX", "ULNA", "ULUS", "ULVA", "UMBO", "UMPH", "UMPS", "UMPY", "UNAI", "UNAU", "UNBE", "UNCE", "UNCI", "UNCO", "UNDE", "UNDO", "UNDY", "UNIS", "UNIT", "UNTO", "UPAS", "UPBY", "UPDO", "UPGO", "UPON", "UPSY", "UPTA", "URAO", "URBS", "URDE", "URDS", "URDY", "UREA", "URES", "URGE", "URIC", "URNS", "URPS", "URSA", "URUS", "URVA", "USED", "USER", "USES", "UTAS", "UTES", "UTIS", "UTUS", "UVAE", "UVAS", "UVEA", "VACS", "VADE", "VAES", "VAGI", "VAGS", "VAIL", "VAIN", "VAIR", "VALE", "VALI", "VAMP", "VANE", "VANG", "VANS", "VANT", "VARA", "VARE", "VARS", "VARY", "VASA", "VASE", "VAST", "VATS", "VATU", "VAUS", "VAUT", "VAVS", "VAWS", "VEAL", "VEEP", "VEER", "VEES", "VEGA", "VEGO", "VEHM", "VEIL", "VEIN", "VELA", "VELD", "VELE", "VELL", "VENA", "VEND",
            "VENT", "VERA", "VERB", "VERD", "VERS", "VERT", "VERY", "VEST", "VETO", "VETS", "VEXT", "VIAE", "VIAL", "VIAS", "VIBE", "VIBS", "VICE", "VIDE", "VIDS", "VIED", "VIER", "VIES", "VIEW", "VIGA", "VIGS", "VILD", "VILE", "VILL", "VIMS", "VINA", "VINE", "VINO", "VINS", "VINT", "VINY", "VIOL", "VIRE", "VIRL", "VISA", "VISE", "VITA", "VITE", "VIVA", "VIVE", "VIVO", "VIZY", "VLEI", "VOAR", "VOES", "VOID", "VOLA", "VOLE", "VOLK", "VOLS", "VOLT", "VORS", "VOTE", "VOWS", "VRIL", "VROT", "VROU", "VROW", "VUGG", "VUGH", "VUGS", "VULN", "VUMS", "WAAC", "WABS", "WACK", "WADD", "WADE", "WADI", "WADS", "WADT", "WADY", "WAES", "WAFF", "WAFT", "WAGE", "WAGS", "WAID", "WAIF", "WAIL", "WAIN", "WAIR", "WAIS", "WAIT", "WAKA", "WAKE", "WAKF", "WALD", "WALE", "WALI", "WALK", "WALL", "WALY", "WAME", "WAND", "WANE", "WANG", "WANK", "WANS", "WANT", "WANY", "WAPS", "WAQF", "WARB", "WARD", "WARE", "WARK", "WARM", "WARN", "WARP", "WARS", "WART", "WARY", "WASE", "WASH", "WASM", "WASP", "WAST", "WATE", "WATS", "WATT", "WAUK", "WAUL", "WAUR", "WAVE", "WAVY", "WAWA", "WAWE", "WAWL", "WAWS", "WAXY", "WAYS", "WEAK", "WEAL", "WEAN", "WEAR", "WEBS", "WEDS", "WEED", "WEEK", "WEEL", "WEEM", "WEEN", "WEEP", "WEER", "WEES", "WEET", "WEFT", "WEID", "WEIL", "WEIR", "WEKA", "WELD", "WELK", "WELL", "WELT", "WEMB", "WEMS", "WENA", "WEND", "WENS", "WENT", "WEPT", "WERE", "WERO", "WERT", "WEST", "WETA", "WETS", "WEXE", "WEYS", "WHAE", "WHAM", "WHAP", "WHAT", "WHEE", "WHEN", "WHET", "WHEW", "WHEY", "WHID", "WHIG", "WHIM", "WHIN", "WHIO", "WHIP", "WHIR", "WHIT", "WHIZ", "WHOA", "WHOM", "WHOP", "WHOT", "WHOW", "WHUP", "WHYS", "WICE", "WICH", "WICK", "WIDE", "WIEL", "WIFE", "WIGS", "WILD", "WILE", "WILI", "WILL", "WILT", "WILY", "WIMP", "WIND", "WINE", "WING", "WINK", "WINN", "WINO", "WINS", "WINY", "WIPE", "WIRE", "WIRY", "WISE", "WISH", "WISP", "WISS", "WIST", "WITE", "WITH", "WITS", "WIVE", "WOAD", "WOCK", "WOES", "WOFS", "WOGS", "WOKE", "WOKS", "WOLD", "WOLF", "WOMB", "WONK", "WONS", "WONT", "WOOD", "WOOF", "WOOL", "WOON", "WOOS", "WOOT", "WOPS", "WORD", "WORE", "WORK", "WORM", "WORN", "WORT", "WOST", "WOTS", "WOVE", "WOWF", "WOWS", "WRAP", "WREN", "WRIT", "WUDS", "WUDU", "WULL", "WUSS", "WYCH", "WYES", "WYLE", "WYND", "WYNN", "WYNS", "WYTE", "XYST", "YAAR", "YABA", "YACK", "YADS", "YAFF", "YAGI", "YAGS", "YAHS", "YAKS", "YALD", "YALE", "YAMS", "YANG", "YANK", "YAPP", "YAPS", "YARD", "YARE", "YARK", "YARN", "YARR", "YATE", "YAUD", "YAUP", "YAWL", "YAWN", "YAWP", "YAWS", "YAWY", "YAYS", "YBET", "YEAD", "YEAH", "YEAN", "YEAR", "YEAS", "YEBO", "YECH", "YEDE", "YEED", "YEGG", "YELD", "YELK", "YELL", "YELM", "YELP", "YELT", "YENS", "YEPS", "YERD", "YERK", "YESK", "YEST", "YETI", "YETT", "YEUK", "YEVE", "YEWS", "YGOE", "YIDS", "YIKE", "YILL", "YINS", "YIPE", "YIPS", "YIRD", "YIRK", "YIRR", "YITE", "YLEM", "YLKE", "YMPE", "YMPT", "YOBS", "YOCK", "YODE", "YODH", "YODS", "YOGA", "YOGH", "YOGI", "YOKE", "YOKS", "YOLD", "YOLK", "YOMP", "YOND", "YONI", "YONT", "YOOF", "YOOP", "YORE", "YORK", "YORP", "YOUK", "YOUR", "YOUS", "YOWE", "YOWL", "YOWS", "YUAN", "YUCA", "YUCH", "YUCK", "YUFT", "YUGA", "YUGS", "YUKE", "YUKO", "YUKS", "YUKY", "YULE", "YUMP", "YUNX", "YUPS", "YURT", "YUTZ", "YUZU", "YWIS", "ZACK", "ZAGS", "ZANY", "ZAPS", "ZARF", "ZATI", "ZEAL", "ZEAS", "ZEBU", "ZEDS", "ZEES", "ZEIN", "ZEKS", "ZELS", "ZEPS", "ZERK", "ZERO", "ZEST", "ZETA", "ZEZE", "ZHOS", "ZIFF", "ZIGS", "ZILA", "ZILL", "ZIMB", "ZINC", "ZINE", "ZING", "ZINS", "ZIPS", "ZITE", "ZITI", "ZITS", "ZIZZ", "ZOBO", "ZOBU", "ZOEA", "ZOIC", "ZOLS", "ZONA", "ZONE", "ZONK", "ZOOM", "ZOON", "ZOOS", "ZOOT", "ZORI", "ZOUK", "ZULU", "ZUPA", "ZURF", "ZYGA", "ZYME", "ZZZS"};
            int x = rnd.Next(nameArray.Length);            // Gets a random word from the array
            int y = rnd.Next(nameArray.Length);            // As above ^
            string pickName = nameArray[x] + nameArray[y]; // Places both words into a string

            byte[] nameBytes = Encoding.ASCII.GetBytes(pickName); // Encodes the string into ASCII byte values
            nameBytes[0] -= 0x20; // Capitalises the first letter

            return nameBytes;
        }

        public static void Log(int logMessage, TextWriter w)
        {
            w.Write("\r\nSeed Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}
