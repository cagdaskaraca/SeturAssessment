using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;

namespace SeturAssessment
{
    public class DatabaseQuerys
    {

        //        CREATE TABLE IF NOT EXISTS contacts(
        //    id UUID PRIMARY KEY,
        //        name VARCHAR(100) NOT NULL,
        //    surname VARCHAR(100) NOT NULL,
        //    company VARCHAR(150)
        //);

        //-- Tablo: contact_infos
        //CREATE TABLE IF NOT EXISTS contact_infos(
        //        id UUID PRIMARY KEY,
        //        contact_id UUID NOT NULL,
        //        type INT NOT NULL, -- 1: Phone, 2: Email, 3: Location
        //    content TEXT NOT NULL,
        //    FOREIGN KEY (contact_id) REFERENCES contacts(id) ON DELETE CASCADE
        //);

        //-- Örnek Veriler
        //INSERT INTO contacts(id, name, surname, company)
        //VALUES
        //  ('11111111-1111-1111-1111-111111111111', 'Çağdaş', 'Karaca', 'Yazılım Ltd.'),
        //  ('22222222-2222-2222-2222-222222222222', 'Ayşe', 'Yılmaz', 'Tasarım AŞ');

        //INSERT INTO contact_infos(id, contact_id, type, content)
        //VALUES
        //  ('aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1', '11111111-1111-1111-1111-111111111111', 1, '+90 555 111 1111'),
        //  ('aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2', '11111111-1111-1111-1111-111111111111', 3, 'İstanbul'),
        //  ('bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1', '22222222-2222-2222-2222-222222222222', 2, 'ayse@example.com');
    }

}
