using System;

namespace RS {
    public class Subaccount {
    public static int HAS_CREDIT = 0;
    public static int USE_SUPER_CREDIT = 1;
    public static int USE_BOTH_CREDITS = 2;
	private int credit_mode;
	private String company_name;
	private String fiscal_code;
	private String vat_number;
	private String name;
	private String surname;
	private String email;
	private String address;
	private String city;
	private String province;
	private String zip;
	private String mobile;
	private String login;
	private String password;
	private Boolean active;
	private SUBACCOUNT_TYPE subaccount_type = DEFAULT_SUBACCOUNT_TYPE;

	public int Credit_mode {
        get { return this.credit_mode; }
        set { this.credit_mode = value; }
	}

	public void setSubaccountHasCredits() {
		this.credit_mode = HAS_CREDIT;
	}
	public void setSubaccountUseSuperaccountCredit() {
		this.credit_mode = USE_SUPER_CREDIT;
	}
    public void setSubaccountUseBothCredits()
    {
        this.credit_mode = USE_BOTH_CREDITS;
    }
	public String Company_name {
        get { return this.company_name; }
        set { this.company_name = value; }
	}
	public String Fiscal_code {
        get { return this.fiscal_code; }
        set { this.fiscal_code = value; }
	}
	public String Vat_number {
        get { return this.vat_number; }
        set { this.vat_number = value; }
	}
    public String Name
    {
        get { return this.name; }
        set { this.name = value; }
	}
	public String Surname {
        get { return this.surname; }
        set { this.surname = value; }
	}
	public String Email {
        get { return this.email; }
        set { this.email = value; }
	}
    public String Address
    {
        get { return this.address; }
        set { this.address = value; }
	}
	public String City {
        get { return this.city; }
        set { this.city = value; }
	}
    public String Province
    {
        get { return this.province; }
        set { this.province = value; }
	}
	public String Zip {
        get { return this.zip; }
        set { this.zip = value; }
	}
	public String Mobile {
        get { return this.mobile; }
        set { this.mobile = value; }
	}
	public String Login {
        get { return this.login; }
        set { this.login = value; }
	}
	public String Password {
        get { return this.password; }
        set { this.password = value; }
	}
	public Boolean Active {
        get { return this.active; }
        set { this.active = value; }
	}
    public String SubaccountType
    {
        get { return this.subaccount_type.ToString().ToLower(); }
        set
        {
            if (value != null && value != "")
            {
                try
                {
                    this.subaccount_type = (SUBACCOUNT_TYPE)Enum.Parse(typeof(SUBACCOUNT_TYPE), value.ToUpper(), true);
                }
                catch (Exception)
                {
                    this.subaccount_type = DEFAULT_SUBACCOUNT_TYPE;
                }
            }
        }
	}

	public enum SUBACCOUNT_TYPE { COMPANY,PRIVATE }

	private static SUBACCOUNT_TYPE DEFAULT_SUBACCOUNT_TYPE = SUBACCOUNT_TYPE.COMPANY;

    }
}
