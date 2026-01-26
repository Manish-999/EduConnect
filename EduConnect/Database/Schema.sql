-- PostgreSQL Database Schema for AcademicEye
-- Database: academiceye
-- Version: 1.0

-- Create Schools table
CREATE TABLE IF NOT EXISTS schools (
    id SERIAL PRIMARY KEY,
    school_name VARCHAR(255) NOT NULL,
    school_type VARCHAR(100),
    affiliation_number VARCHAR(100),
    school_code VARCHAR(50) UNIQUE,
    medium_of_instruction VARCHAR(50),
    total_students INTEGER DEFAULT 0,
    total_teachers INTEGER DEFAULT 0,
    academic_year_start DATE,
    address_line TEXT,
    city VARCHAR(100),
    district VARCHAR(100),
    state VARCHAR(100),
    pin_code VARCHAR(20),
    country VARCHAR(100) DEFAULT 'India',
    principal_name VARCHAR(255),
    designation VARCHAR(100),
    mobile VARCHAR(20),
    email VARCHAR(255),
    aadhar VARCHAR(20),
    password_hash VARCHAR(255),
    terms_accepted BOOLEAN DEFAULT FALSE,
    school_logo_path VARCHAR(500),
    affiliation_certificate_path VARCHAR(500),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Teachers table
CREATE TABLE IF NOT EXISTS teachers (
    id SERIAL PRIMARY KEY,
    school_id INTEGER REFERENCES schools(id) ON DELETE CASCADE,
    employee_id VARCHAR(100),
    first_name VARCHAR(100) NOT NULL,
    middle_name VARCHAR(100),
    last_name VARCHAR(100) NOT NULL,
    date_of_birth DATE NOT NULL,
    gender VARCHAR(20) NOT NULL,
    aadhar_number VARCHAR(20) NOT NULL,
    nationality VARCHAR(100),
    blood_group VARCHAR(10),
    marital_status VARCHAR(20),
    country VARCHAR(100) DEFAULT 'India',
    email VARCHAR(255),
    mobile_number VARCHAR(20) NOT NULL,
    alternate_mobile VARCHAR(20),
    permanent_address TEXT NOT NULL,
    current_address TEXT,
    city VARCHAR(100) NOT NULL,
    district VARCHAR(100) NOT NULL,
    state VARCHAR(100) NOT NULL,
    pin_code VARCHAR(20) NOT NULL,
    designation VARCHAR(100) NOT NULL,
    department VARCHAR(100),
    subjects TEXT,
    qualification VARCHAR(255) NOT NULL,
    specialization VARCHAR(255),
    experience VARCHAR(100),
    joining_date DATE NOT NULL,
    employment_type VARCHAR(50),
    highest_qualification VARCHAR(255) NOT NULL,
    university VARCHAR(255) NOT NULL,
    year_of_passing INTEGER NOT NULL,
    percentage VARCHAR(20),
    additional_certifications TEXT,
    previous_school VARCHAR(255),
    previous_designation VARCHAR(100),
    previous_experience VARCHAR(100),
    reason_for_leaving TEXT,
    basic_salary DECIMAL(10, 2) DEFAULT 0,
    allowances DECIMAL(10, 2) DEFAULT 0,
    bank_name VARCHAR(255),
    account_number VARCHAR(50),
    ifsc_code VARCHAR(20),
    pan_number VARCHAR(20),
    emergency_contact_name VARCHAR(255) NOT NULL,
    emergency_relation VARCHAR(50),
    emergency_mobile VARCHAR(20) NOT NULL,
    emergency_address TEXT,
    terms_accepted BOOLEAN DEFAULT FALSE,
    photo_path VARCHAR(500),
    resume_path VARCHAR(500),
    aadhar_card_path VARCHAR(500),
    certificates_path VARCHAR(500),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Students table
CREATE TABLE IF NOT EXISTS students (
    id SERIAL PRIMARY KEY,
    school_id INTEGER REFERENCES schools(id) ON DELETE CASCADE,
    first_name VARCHAR(100) NOT NULL,
    middle_name VARCHAR(100),
    last_name VARCHAR(100) NOT NULL,
    date_of_birth DATE NOT NULL,
    gender VARCHAR(20) NOT NULL,
    aadhar_number VARCHAR(20) NOT NULL,
    category VARCHAR(50),
    religion VARCHAR(50),
    nationality VARCHAR(100),
    blood_group VARCHAR(10),
    mother_tongue VARCHAR(50),
    previous_school_name VARCHAR(255),
    class_applying_for VARCHAR(50) NOT NULL,
    medium_of_instruction VARCHAR(50),
    father_name VARCHAR(255) NOT NULL,
    mother_name VARCHAR(255) NOT NULL,
    guardian_name VARCHAR(255) NOT NULL,
    occupation VARCHAR(100),
    educational_qualification VARCHAR(255),
    annual_income VARCHAR(100),
    father_mobile VARCHAR(20) NOT NULL,
    mother_mobile VARCHAR(20),
    parent_email VARCHAR(255),
    parent_aadhar VARCHAR(20),
    country VARCHAR(100) DEFAULT 'India',
    permanent_address TEXT NOT NULL,
    current_address TEXT,
    city VARCHAR(100) NOT NULL,
    district VARCHAR(100) NOT NULL,
    state VARCHAR(100) NOT NULL,
    pin_code VARCHAR(20) NOT NULL,
    previous_class_passed VARCHAR(50),
    previous_school VARCHAR(255),
    board VARCHAR(100),
    marks_obtained VARCHAR(50),
    tc_number VARCHAR(100),
    migration_certificate VARCHAR(255),
    special_needs VARCHAR(50),
    special_needs_detail TEXT,
    emergency_contact VARCHAR(20) NOT NULL,
    sibling_in_school VARCHAR(10),
    transport_required VARCHAR(10),
    hostel_required VARCHAR(10),
    parent_signature TEXT,
    photo_path VARCHAR(500),
    birth_certificate_path VARCHAR(500),
    student_aadhar_path VARCHAR(500),
    parent_aadhar_doc_path VARCHAR(500),
    report_card_path VARCHAR(500),
    transfer_certificate_path VARCHAR(500),
    caste_certificate_path VARCHAR(500),
    income_certificate_path VARCHAR(500),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create indexes for better query performance
CREATE INDEX IF NOT EXISTS idx_teachers_school_id ON teachers(school_id);
CREATE INDEX IF NOT EXISTS idx_students_school_id ON students(school_id);
CREATE INDEX IF NOT EXISTS idx_teachers_employee_id ON teachers(employee_id);
CREATE INDEX IF NOT EXISTS idx_schools_school_code ON schools(school_code);
CREATE INDEX IF NOT EXISTS idx_teachers_email ON teachers(email);
CREATE INDEX IF NOT EXISTS idx_students_aadhar ON students(aadhar_number);
CREATE INDEX IF NOT EXISTS idx_teachers_aadhar ON teachers(aadhar_number);

-- Create function to update updated_at timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Create triggers to automatically update updated_at
CREATE TRIGGER update_schools_updated_at BEFORE UPDATE ON schools
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_teachers_updated_at BEFORE UPDATE ON teachers
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_students_updated_at BEFORE UPDATE ON students
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

