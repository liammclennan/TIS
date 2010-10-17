
class ConfigTasks

	def self.set_app_setting(config_file, key, value)
		ovsd_element = config_file.root.elements['appSettings'].get_elements("add[@key='#{key}']")[0]
		ovsd_element.attributes['value'] = value
	end
	
	def self.set_connection_string(config_file, name, connection_string)
		conn_string_element = config_file.root.elements['connectionStrings'].get_elements("add[@name='#{name}']")[0]
		conn_string_element.attributes['connectionString'] = connection_string
	end
	
	def self.set_debug_compilation(config_file, debug_compilation)
		compilation_element = config_file.root.elements['system.web'].get_elements("compilation")[0]
		compilation_element.attributes['debug'] = false
	end
	
	private
	
	def self.write_xml_to_file(xml_document, file)
		File.open(file, 'w') do |config_file|
			formatter = REXML::Formatters::Default.new
			formatter.write(xml_document, config_file)
		end
	end

end